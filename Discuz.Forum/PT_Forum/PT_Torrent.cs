using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.IO;

using Discuz.Common;
using Discuz.Entity;

namespace Discuz.Forum
{
    public class PTTorrent
    {
        /// <summary>
        /// 返回值 -1出错，0结束符e，1字符串1:x，2整数i..e，3列表l..e，4字典d..e
        /// </summary>
        /// <param name="input">文件</param>
        /// <param name="position">文件的读取位置</param>
        /// <param name="SectionLong">如果为整数，此处为返回值</param>
        /// <param name="SectionString">如果为字符串，此处为返回值</param>
        /// <returns></returns>
        public static int ReadNext(ref byte[] input, ref int position, out long SectionLong, out string SectionString)
        {
            SectionLong = 0;
            SectionString = "";

            try
            {
                //结束符
                if (input[position] == (byte)'e')
                {
                    position++;
                    return 0;
                }

                //列表
                if (input[position] == (byte)'l')
                {
                    position++;
                    return 3;
                }

                //字典
                if (input[position] == (byte)'d')
                {
                    position++;
                    return 4;
                }

                //整数
                if (input[position] == (byte)'i')
                {
                    //搜索结束符e
                    int a = Array.IndexOf(input, (byte)'e', position + 1);
                    //截取整数字符串
                    string b = System.Text.Encoding.UTF8.GetString(input, position + 1, a - position - 1);
                    SectionLong = long.Parse(b);

                    position = a + 1;
                    return 2;
                }

                //字符串
                if ((int)input[position] >= (int)'0' && (int)input[position] <= (int)'9')
                {
                    //搜索字符串中间符号“:”
                    int a = Array.IndexOf(input, (byte)':', position + 1);
                    //截取字符串长度
                    string b = System.Text.Encoding.UTF8.GetString(input, position, a - position);
                    int c = int.Parse(b);
                    SectionString = System.Text.Encoding.UTF8.GetString(input, a + 1, c);
                    SectionLong = c;

                    position = a + c + 1;
                    return 1;
                }

                //其他情况：出错
                return -1;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return -1;
            }
        }



        public static int SaveAbtSeed(byte[] input, int uid, string filename)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();

            string backmessage = "";
            int pos = 0;

            //文件列表段和hash段的位置
            int filelistb = 0;
            int fileliste = 0;
            int pieceb = 0;
            int piecee = 0;

            DataTable filelist = new DataTable();
            filelist.TableName = "seedfile";
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "seedid";
            filelist.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "filename";
            filelist.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Decimal");
            column.ColumnName = "filesize";
            filelist.Columns.Add(column);

            //任何数据初始化初始化放在递归调用之前
            seedinfo.SingleFile = false;

            try
            {
                if (ReadSeed(ref input, ref pos, 1, "", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                {
                    byte[] hashpart;

                    if (seedinfo.SingleFile)
                    {
                        //单文件，只读取pieces部分，其他部分生成
                        string part2s = string.Format("6:lengthi{0}e4:name{1}:{2}12:piece lengthi{3}e6:pieces{4}:", seedinfo.FileSize, System.Text.Encoding.UTF8.GetBytes(seedinfo.FolderName).Length, seedinfo.FolderName, seedinfo.PieceLength, piecee - pieceb + 1);
                        byte[] part2 = System.Text.Encoding.UTF8.GetBytes(part2s);

                        hashpart = new byte[1 + part2.Length + (piecee - pieceb + 1) + 1];

                        hashpart[0] = (byte)'d';
                        hashpart[hashpart.Length - 1] = (byte)'e';
                        Array.Copy(part2, 0, hashpart, 1, part2.Length);
                        Array.Copy(input, pieceb, hashpart, 1 + part2.Length, piecee - pieceb + 1);
                    }
                    else
                    {
                        //多文件，读取files和pieces部分，files部分结构不变
                        //byte[] part1 = new byte[fileliste - filelistb + 1];
                        string part2s = string.Format("4:name{0}:{1}12:piece lengthi{2}e6:pieces{3}:", System.Text.Encoding.UTF8.GetBytes(seedinfo.FolderName).Length, seedinfo.FolderName, seedinfo.PieceLength, piecee - pieceb + 1);
                        byte[] part2 = System.Text.Encoding.UTF8.GetBytes(part2s);
                        //byte[] part3 = new byte[piecee - pieceb + 1];

                        hashpart = new byte[1 + (fileliste - filelistb + 1) + part2.Length + (piecee - pieceb + 1) + 1];

                        hashpart[0] = (byte)'d';
                        hashpart[hashpart.Length - 1] = (byte)'e';
                        Array.Copy(input, filelistb, hashpart, 1, fileliste - filelistb + 1);
                        Array.Copy(part2, 0, hashpart, fileliste - filelistb + 2, part2.Length);
                        Array.Copy(input, pieceb, hashpart, 1 + (fileliste - filelistb + 1) + part2.Length, piecee - pieceb + 1);
                    }

                    //统一添加private项目和随机码
                    byte[] tmp = new byte[hashpart.Length + 60];
                    Array.Copy(hashpart, tmp, hashpart.Length - 1);
                    //input = new byte[57];
                    input = System.Text.Encoding.UTF8.GetBytes("7:privatei1e6:source37:FGBT-" + PTTools.GetRandomString(32) + "e");
                    Array.Copy(input, 0, tmp, hashpart.Length - 1, 61);
                    hashpart = new byte[tmp.Length];
                    Array.Copy(tmp, hashpart, tmp.Length);

                    seedinfo.InfoHash = PTTools.Byte2HEX(new SHA1CryptoServiceProvider().ComputeHash(hashpart));
                    if (seedinfo.InfoHash.Length != 40) return -2;

                    //计算总文件数和文件大小，插入种子文件列表信息
                    seedinfo.FileSize = 0;
                    seedinfo.FileCount = filelist.Rows.Count;
                    foreach (DataRow dr in filelist.Rows)
                    {
                        dr["seedid"] = seedinfo.SeedId;
                        seedinfo.FileSize += Decimal.Parse(dr["filesize"].ToString());
                    }

                    //在数据库中创建种子信息
                    seedinfo.SeedId = PTAbt.AbtInsertSeed(seedinfo.InfoHash, seedinfo.FileCount, seedinfo.FileSize, filename, uid);  
                    if (seedinfo.SeedId < 0) return -5;                     //创建种子失败


                    //完善种子文件，准备保存
                    string headinfo = "";
                    if (seedinfo.CreatedBy.Length > 0) headinfo += "10:created by" + seedinfo.CreatedBy.Length.ToString() + ":" + seedinfo.CreatedBy;
                    headinfo += "13:creation datei" + PTTools.Time2Int(seedinfo.CreatedDate).ToString() + "e8:encoding5:UTF-84:info";
                    byte[] headbyte = System.Text.Encoding.UTF8.GetBytes(headinfo);
                    input = new byte[headbyte.Length + hashpart.Length + 1];
                    Array.Copy(headbyte, input, headbyte.Length);
                    Array.Copy(hashpart, 0, input, headbyte.Length, hashpart.Length);
                    input[headbyte.Length + hashpart.Length] = (byte)'e';


                    //保存文件
                    DateTime savetime = DateTime.Now;
                    string savedir = "D:\\torrentabt\\" + (seedinfo.SeedId / 10000).ToString("000") + "\\" + ((seedinfo.SeedId % 10000) / 100).ToString("00") + "\\";

                    if (!Directory.Exists(savedir)) Utils.CreateDir(savedir);      //检查目录是否存在
                    if (!Directory.Exists(savedir))
                    {
                        //无法创建目录，清理记录
                        PTAbt.AbtDeleteSeed(seedinfo.SeedId);
                        PTAbt.AbtInsertLog(seedinfo.SeedId, uid, 200, "创建种子保存目录失败：" + savedir);
                        return -4;
                    }
                    savedir += seedinfo.SeedId + ".torrent";
                    try
                    {
                        FileStream fs = new FileStream(savedir, FileMode.Create);
                        fs.Write(input, 0, input.Length);
                        fs.Flush();
                        fs.Close();
                    }
                    catch (System.Exception ex)
                    {
                        //保存文件出现问题，清理掉所有已经存在的记录
                        PTAbt.AbtDeleteSeed(seedinfo.SeedId);
                        PTAbt.AbtInsertLog(seedinfo.SeedId, uid, 201, "创建种子文件失败：" + ex.ToString());
                        return -3;
                    }
                    
                    //文件保存正常，更新数据库并返回种子id
                    return seedinfo.SeedId;
                }
                else return -1;
            }
            catch (System.Exception ex)
            {
                PTAbt.AbtInsertLog(seedinfo.SeedId, uid, 202, "创建种子出现异常：" + ex.ToString());
                return -99;
            }
        }


        /// <summary>
        /// 保存种子，注意：此函数会更改seedinfo中的path、status等等，若成功执行，seedinfo.status = 1
        /// </summary>
        /// <param name="input"></param>
        /// <param name="seedinfo"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetSeedInfohash_c(byte[] input)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();

            string backmessage = "";
            int pos = 0;

            //文件列表段和hash段的位置
            int filelistb = 0;
            int fileliste = 0;
            int pieceb = 0;
            int piecee = 0;

            DataTable filelist = new DataTable();
            filelist.TableName = "seedfile";
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "seedid";
            filelist.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "filename";
            filelist.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Decimal");
            column.ColumnName = "filesize";
            filelist.Columns.Add(column);

            //任何数据初始化初始化放在递归调用之前
            seedinfo.SingleFile = false;

            try
            {
                if (ReadSeed(ref input, ref pos, 1, "", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                {
                    byte[] hashpart;

                    if (seedinfo.SingleFile)
                    {
                        //单文件，只读取pieces部分，其他部分生成
                        string part2s = string.Format("6:lengthi{0}e4:name{1}:{2}12:piece lengthi{3}e6:pieces{4}:", seedinfo.FileSize, System.Text.Encoding.UTF8.GetBytes(seedinfo.FolderName).Length, seedinfo.FolderName, seedinfo.PieceLength, piecee - pieceb + 1);
                        byte[] part2 = System.Text.Encoding.UTF8.GetBytes(part2s);

                        hashpart = new byte[1 + part2.Length + (piecee - pieceb + 1) + 1];

                        hashpart[0] = (byte)'d';
                        hashpart[hashpart.Length - 1] = (byte)'e';
                        Array.Copy(part2, 0, hashpart, 1, part2.Length);
                        Array.Copy(input, pieceb, hashpart, 1 + part2.Length, piecee - pieceb + 1);
                    }
                    else
                    {
                        //多文件，读取files和pieces部分，files部分结构不变
                        //byte[] part1 = new byte[fileliste - filelistb + 1];
                        string part2s = string.Format("4:name{0}:{1}12:piece lengthi{2}e6:pieces{3}:", System.Text.Encoding.UTF8.GetBytes(seedinfo.FolderName).Length, seedinfo.FolderName, seedinfo.PieceLength, piecee - pieceb + 1);
                        byte[] part2 = System.Text.Encoding.UTF8.GetBytes(part2s);
                        //byte[] part3 = new byte[piecee - pieceb + 1];

                        hashpart = new byte[1 + (fileliste - filelistb + 1) + part2.Length + (piecee - pieceb + 1) + 1];

                        hashpart[0] = (byte)'d';
                        hashpart[hashpart.Length - 1] = (byte)'e';
                        Array.Copy(input, filelistb, hashpart, 1, fileliste - filelistb + 1);
                        Array.Copy(part2, 0, hashpart, fileliste - filelistb + 2, part2.Length);
                        Array.Copy(input, pieceb, hashpart, 1 + (fileliste - filelistb + 1) + part2.Length, piecee - pieceb + 1);
                    }

                    //计算原始Hash值
                    seedinfo.InfoHash_c = PTTools.Byte2HEX(new SHA1CryptoServiceProvider().ComputeHash(hashpart));
                    if (seedinfo.InfoHash_c.Length != 40) return "ERROR";

                    return seedinfo.InfoHash_c;
                }
                else return "ERROR";
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return "ERROR";
            }
        }

        /// <summary>
        /// 保存种子，注意：此函数会更改seedinfo中的path、status等等，若成功执行，seedinfo.status = 1
        /// </summary>
        /// <param name="input"></param>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public static int SaveSeed(byte[] input, ref PTSeedinfo seedinfo)
        {
            return SaveSeed(input, ref seedinfo, "");
        }

        /// <summary>
        /// 保存种子，注意：此函数会更改seedinfo中的path、status等等，若成功执行，seedinfo.status = 1
        /// 若种子出现重复未修改，则返回值为0，返回正数：创建的种子id或编辑成功的种子id，返回负数：出现问题
        /// 函数正常返回后，种子状态为1
        /// </summary>
        /// <param name="input"></param>
        /// <param name="seedinfo"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int SaveSeed(byte[] input, ref PTSeedinfo seedinfo, string path)
        {
            string backmessage = "";
            int pos = 0;

            //文件列表段和hash段的位置
            int filelistb = 0;
            int fileliste = 0;
            int pieceb = 0;
            int piecee = 0;

            DataTable filelist = new DataTable();
            filelist.TableName = "seedfile";
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "seedid";
            filelist.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "filename";
            filelist.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Decimal");
            column.ColumnName = "filesize";
            filelist.Columns.Add(column);

            //任何数据初始化初始化放在递归调用之前
            seedinfo.SingleFile = false;
            seedinfo.FileCount = 0;
            seedinfo.FileName = "";
            //若此处不清空，则当多文件种子编辑为单文件时，将保留上一次的文件夹名作为单文件的文件名，造成错误
            seedinfo.FolderName = ""; 
            seedinfo.InfoHash = "";
            seedinfo.InfoHash_c = "";

            try
            {
                if (ReadSeed(ref input, ref pos, 1, "", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                {
                    byte[] hashpart;

                    if (seedinfo.SingleFile)
                    {
                        //单文件，只读取pieces部分，其他部分生成
                        string part2s = string.Format("6:lengthi{0}e4:name{1}:{2}12:piece lengthi{3}e6:pieces{4}:", seedinfo.FileSize, System.Text.Encoding.UTF8.GetBytes(seedinfo.FolderName).Length, seedinfo.FolderName, seedinfo.PieceLength, piecee - pieceb + 1);
                        byte[] part2 = System.Text.Encoding.UTF8.GetBytes(part2s);

                        hashpart = new byte[1 + part2.Length + (piecee - pieceb + 1) + 1];

                        hashpart[0] = (byte)'d';
                        hashpart[hashpart.Length - 1] = (byte)'e';
                        Array.Copy(part2, 0, hashpart, 1, part2.Length);
                        Array.Copy(input, pieceb, hashpart, 1 + part2.Length, piecee - pieceb + 1);
                    }
                    else
                    {
                        //多文件，读取files和pieces部分，files部分结构不变
                        //byte[] part1 = new byte[fileliste - filelistb + 1];
                        string part2s = string.Format("4:name{0}:{1}12:piece lengthi{2}e6:pieces{3}:", System.Text.Encoding.UTF8.GetBytes(seedinfo.FolderName).Length, seedinfo.FolderName, seedinfo.PieceLength, piecee - pieceb + 1);
                        byte[] part2 = System.Text.Encoding.UTF8.GetBytes(part2s);
                        //byte[] part3 = new byte[piecee - pieceb + 1];

                        hashpart = new byte[1 + (fileliste - filelistb + 1) + part2.Length + (piecee - pieceb + 1) + 1];

                        hashpart[0] = (byte)'d';
                        hashpart[hashpart.Length - 1] = (byte)'e';
                        Array.Copy(input, filelistb, hashpart, 1, fileliste - filelistb + 1);
                        Array.Copy(part2, 0, hashpart, fileliste - filelistb + 2, part2.Length);
                        Array.Copy(input, pieceb, hashpart, 1 + (fileliste - filelistb + 1) + part2.Length, piecee - pieceb + 1);
                    }

                    //计算原始Hash值
                    seedinfo.InfoHash_c = PTTools.Byte2HEX(new SHA1CryptoServiceProvider().ComputeHash(hashpart));
                    if (seedinfo.InfoHash_c.Length != 40) return -2;

                    //统一添加private项目和随机码
                    byte[] tmp = new byte[hashpart.Length + 60];
                    Array.Copy(hashpart, tmp, hashpart.Length - 1);
                    //input = new byte[57];
                    input = System.Text.Encoding.UTF8.GetBytes("7:privatei1e6:source37:FGBT-" + PTTools.GetRandomString(32) + "e");
                    Array.Copy(input, 0, tmp, hashpart.Length - 1, 61);
                    hashpart = new byte[tmp.Length];
                    Array.Copy(tmp, hashpart, tmp.Length);

                    //计算FGBT Hash值
                    seedinfo.InfoHash = PTTools.Byte2HEX(new SHA1CryptoServiceProvider().ComputeHash(hashpart));
                    if (seedinfo.InfoHash.Length != 40) return -2;




                    //检测是否存在当前种子
                    PTSeedinfo oldseedinfo = PTSeeds.GetSeedInfoFullAllStatus(seedinfo.InfoHash_c);
                    
                    //新创建的种子存在重复
                    if (oldseedinfo.SeedId > 0 && seedinfo.SeedId < 1)
                    {
                        seedinfo.SeedId = oldseedinfo.SeedId;
                        seedinfo.TopicId = oldseedinfo.TopicId;
                        seedinfo.Status = oldseedinfo.Status;
                        return 0;
                    }
                    //编辑种子，种子没有变化的情况
                    else if (oldseedinfo.SeedId > 0 && seedinfo.SeedId > 0 && path != "" && oldseedinfo.SeedId == seedinfo.SeedId)
                    {
                        //更新其他需要更新的信息，在编辑页面更新。。
                        //PTSeeds.UpdateSeedEditWithSeed(seedinfo);
                        return 0;
                    }
                    //编辑种子，不是本身的重复种子
                    else if (oldseedinfo.SeedId > 0 && seedinfo.SeedId > 0 && path != "" && oldseedinfo.SeedId != seedinfo.SeedId)
                    {
                        seedinfo.SeedId = oldseedinfo.SeedId;
                        seedinfo.TopicId = oldseedinfo.TopicId;
                        seedinfo.Status = oldseedinfo.Status;
                        return 0;
                    }


                    //如果不是修改种子的话，在数据库中创建种子信息
                    try
                    {
                        if (path == "") seedinfo.SeedId = PTSeeds.CreateSeed(seedinfo);
                    }
                    catch (System.Exception ex)
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.TorrentFile, PTLog.LogStatus.Exception, "SaveSeed_InsertData", ex.ToString());
                        return -15;
                    }
                    if (seedinfo.SeedId < 0) return -5; //创建种子失败


                    //清理已经存在的文件列表，插入种子文件列表信息
                    if (path != "")                                        
                    {
                        PrivateBT.DeleteSeedFileInfo(seedinfo.SeedId);
                        seedinfo.FileCount = 0;
                        seedinfo.FileSize = 0;
                    }
                    seedinfo.FileSize = 0; //计算总文件数和文件大小
                    seedinfo.FileCount = filelist.Rows.Count;
                    foreach (DataRow dr in filelist.Rows)
                    {
                        dr["seedid"] = seedinfo.SeedId;
                        seedinfo.FileSize += Decimal.Parse(dr["filesize"].ToString());
                    }
                    PrivateBT.InsertSeedFileList(filelist);


                    //完善种子文件，准备保存
                    string headinfo = "";
                    if (seedinfo.CreatedBy.Length > 0) headinfo += "10:created by" + seedinfo.CreatedBy.Length.ToString() + ":" + seedinfo.CreatedBy;
                    headinfo += "13:creation datei" + PTTools.Time2Int(seedinfo.CreatedDate).ToString() + "e8:encoding5:UTF-84:info";
                    byte[] headbyte = System.Text.Encoding.UTF8.GetBytes(headinfo);
                    input = new byte[headbyte.Length + hashpart.Length + 1];
                    Array.Copy(headbyte, input, headbyte.Length);
                    Array.Copy(hashpart, 0, input, headbyte.Length, hashpart.Length);
                    input[headbyte.Length + hashpart.Length] = (byte)'e';


                    //保存文件
                    DateTime savetime = DateTime.Now;
                    string savedir = "D:\\torrent\\" + savetime.ToString("yyyy") + "\\" + savetime.ToString("MM") + "\\";
                    if (path == "")
                    {
                        savedir = "D:\\torrent\\" + savetime.ToString("yyyy") + "\\" + savetime.ToString("MM") + "\\";
                        if (!Directory.Exists(savedir)) Utils.CreateDir(savedir);      //检查目录是否存在
                        if (!Directory.Exists(savedir))
                        {
                            //无法创建目录，清理记录
                            PrivateBT.DeleteSeedFileInfo(seedinfo.SeedId);
                            PTSeeds.UpdateSeedStatus(seedinfo.SeedId, -1);
                            PTLog.InsertSystemLog(PTLog.LogType.TorrentFile, PTLog.LogStatus.Error, "SaveSeed_14", "无法创建：" + savedir);
                            return -14;
                        }
                        savedir += seedinfo.SeedId + ".torrent";
                        try
                        {
                            FileStream fs = new FileStream(savedir, FileMode.Create);
                            fs.Write(input, 0, input.Length);
                            fs.Flush();
                            fs.Close();
                        }
                        catch (System.Exception ex)
                        {
                            //保存文件出现问题，清理掉所有已经存在的记录
                            PTLog.InsertSystemLog(PTLog.LogType.TorrentFile, PTLog.LogStatus.Exception, "SaveSeed_3", ex.ToString());
                            PrivateBT.DeleteSeedFileInfo(seedinfo.SeedId);
                            PTSeeds.UpdateSeedStatus(seedinfo.SeedId, -1);
                            return -3;
                        }
                    }
                    else
                    {
                        savedir = path;
                        if (path.Length > 8) path = path.Substring(0, path.Length - 8);
                        if (path.LastIndexOf("\\") < 0) return -4;
                        path = path.Substring(0, path.LastIndexOf("\\"));
                        if (!Directory.Exists(path))
                        {
                            //无法创建目录，清理记录
                            //DeleteSeedFileInfo(seedinfo.SeedId);
                            //DeleteSeed(seedinfo.SeedId);
                            return -4;
                        }
                        //savedir += seedinfo.SeedId + "." + savetime.ToString("u").Replace(":", "").Replace(" ", "") +  ".torrent";
                        try
                        {
                            File.Delete(savedir); //删掉原来的种子文件
                        }
                        catch (System.Exception ex)
                        {
                            //可能是文件不存在
                            PTLog.InsertSystemLog(PTLog.LogType.TorrentFile, PTLog.LogStatus.Exception, "SaveSeed_None", ex.ToString());
                        }
                        try
                        {
                            FileStream fs = new FileStream(savedir, FileMode.Create);
                            fs.Write(input, 0, input.Length);
                            fs.Flush();
                            fs.Close();
                        }
                        catch (System.Exception ex)
                        {
                            //保存文件出现问题，清理掉所有已经存在的记录,但是不能删掉主题ID和种子ID
                            PTLog.InsertSystemLog(PTLog.LogType.TorrentFile, PTLog.LogStatus.Exception, "SaveSeed_5", ex.ToString());
                            PrivateBT.DeleteSeedFileInfo(seedinfo.SeedId);
                            //DeleteSeed(seedinfo.SeedId);
                            return -5;
                        }
                    }

                    //文件保存正常，更新数据库并返回种子id，种子状态为1
                    seedinfo.Path = savedir;
                    seedinfo.Status = 1;
                    seedinfo.UploadRatio = PTTools.GetUploadRatio(seedinfo.FileSize);
                    //if (PTTools.GetDownloadRatio(seedinfo.FileSize) < seedinfo.DownloadRatio) seedinfo.DownloadRatio = PTTools.GetDownloadRatio(seedinfo.FileSize);
                    PTSeeds.UpdateSeedEditWithSeed(seedinfo);
                    return seedinfo.SeedId;
                }
                else return -1;
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.TorrentFile, PTLog.LogStatus.Exception, "SaveSeed_99", ex.ToString());
                return -99;
            }
        }


        public static int ReadSeed(ref byte[] input, ref int position, int level, string LevelPreName, ref PTSeedinfo seedinfo, out string MessageBack, ref DataTable filelist, ref int filelistb, ref int fileliste, ref int pieceb, ref int piecee)
        {
            int i = 0;
            int PreType = 0;
            string PreName = "";
            long SectionLong = 0;
            string SectionString = "";
            MessageBack = "";

            //文件列表处理需要
            long tmpFileSize = 0;
            string tmpFilePath = "";

            //函数中不应对seedinfo数值进行初始化，否则会被递归调用修改！！！递归调用切记！！
            //seedinfo.SingleFile = false; 【错误】

            try
            {
                for (i = 0; i < 65535; i++)
                {
                    PreType = ReadNext(ref input, ref position, out SectionLong, out SectionString);

                    //出错
                    if (PreType < 0) return -1;

                    //结束符e
                    else if (PreType == 0)
                    {
                        //递归调用层，返回
                        if (level > 2)
                        {
                            //第五层结束时，增加文件列表信息
                            if (level == 5 && LevelPreName == ".<null>.info.files.<null>")
                            {
                                DataRow dr = filelist.NewRow();
                                dr["filename"] = tmpFilePath;
                                dr["filesize"] = (Decimal)tmpFileSize;
                                filelist.Rows.Add(dr);
                            }
                            else if (level == 4 && LevelPreName == ".<null>.info.files")
                            {
                                fileliste = position - 1;
                            }

                            return i;
                        }
                        else
                        {
                            //第二层，检查位置是否已经移动到文件末尾
                            if (input.Length == position) return 1;
                            else return -1;
                        }
                    }

                    //字符串
                    else if (PreType == 1)
                    {

                        if (level == 2 && LevelPreName == ".<null>")
                        {
                            //第二层，可能包括 announce,announce-list,created by（字符串处理）,encoding, creation date（整数处理）等
                            //项目名
                            if (PreName == "") PreName = SectionString;
                            //数值
                            else
                            {
                                //创建者
                                if (PreName == "created by") seedinfo.CreatedBy = Utils.HtmlEncode(SectionString);
                                PreName = "";
                            }
                        }
                        else if (level == 3 && LevelPreName == ".<null>.info")
                        {
                            //第三层，可能包括files,name（字符串处理）,piece length,pieces,length（单文件情况，整数处理）等
                            //项目名
                            if (PreName == "")
                            {
                                PreName = SectionString;
                                //如果出现length，则为单文件
                                if (PreName == "length")
                                {
                                    seedinfo.SingleFile = true;
                                }
                            }
                            else
                            {
                                //数值
                                //创建者，其他字符串类型数值均不予处理
                                if (PreName == "name")
                                {
                                    if (seedinfo.SingleFile && seedinfo.FileSize != 0 && filelist.Rows.Count < 1)
                                    {
                                        //单个文件的种子，插入文件列表的操作<><><><><>
                                        DataRow dr = filelist.NewRow();
                                        dr["filename"] = SectionString;
                                        dr["filesize"] = seedinfo.FileSize;
                                        filelist.Rows.Add(dr);
                                    }

                                    seedinfo.FolderName = SectionString;
                                }
                                else if (PreName == "pieces")
                                {
                                    piecee = position - 1;
                                    pieceb = position - (int)SectionLong;
                                }
                                PreName = "";
                            }
                        }
                        else if (level == 5 && LevelPreName == ".<null>.info.files.<null>")
                        {
                            //第五层，文件信息层，可能包括length（整数处理）,path等
                            //项目名
                            if (PreName == "") PreName = SectionString;
                            //数值
                            else
                            {
                                //暂时允许...此处不应出现字符串类型的数值
                                PreName = "";
                                //return -1;
                            }
                        }
                        else if (level == 6 && LevelPreName == ".<null>.info.files.<null>.path")
                        {
                            //第六层，文件路径层，可能包括 文件路径列表
                            MessageBack += "/" + SectionString;
                        }

                        else
                        {
                            //暂时允许...其他情况不应该出现//return -1;
                            if (PreName == "") PreName = SectionString;
                            else
                            {
                                PreName = "";
                            }
                        }
                    }

                    //整数
                    else if (PreType == 2)
                    {
                        if (PreName == "")
                        {
                            //不应出现，所有整数类型都必须有项目名
                            //return -1;
                        }
                        else
                        {
                            if (level == 2 && LevelPreName == ".<null>")
                            {
                                if (PreName == "creation date") seedinfo.CreatedDate = PTTools.Int2Time((int)SectionLong);
                                //第二层允许出现其他整数类型，不作处理
                            }
                            else if (level == 3 && LevelPreName == ".<null>.info")
                            {
                                if (PreName == "piece length") seedinfo.PieceLength = (int)SectionLong;
                                if (PreName == "length")
                                {
                                    //单个文件的情况
                                    seedinfo.SingleFile = true;
                                    seedinfo.FileSize = (Decimal)SectionLong;
                                    if (seedinfo.FolderName != "" && filelist.Rows.Count < 1)
                                    {
                                        //单个文件的种子，插入文件列表的操作<><><><><>
                                        DataRow dr = filelist.NewRow();
                                        dr["filename"] = seedinfo.FolderName;
                                        dr["filesize"] = (Decimal)SectionLong;
                                        filelist.Rows.Add(dr);
                                    }
                                }

                                //第三层允许出现其他整数类型，不作处理
                            }
                            else if (level == 5 && LevelPreName == ".<null>.info.files.<null>")
                            {
                                //第五层，文件信息层，获取文件大小
                                if (PreName == "length") tmpFileSize = SectionLong;
                                else
                                {
                                    //文件信息层只允许length出现
                                    //return -1;
                                }
                            }
                            else
                            {
                                //不应该出现的情况，只允许第二、三、五层出现整数类型
                                //return -1;
                            }
                            PreName = "";
                        }

                    }

                    //列表
                    else if (PreType == 3)
                    {
                        //已知的列表项：announce-list,announce-list内列表,files,path，只需要对path和files进行处理
                        if (level == 5 && LevelPreName == ".<null>.info.files.<null>" && PreName == "path")
                        {
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".path", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                //返回的文件路径
                                tmpFilePath = backmessage;
                                PreName = "";
                            }
                            else return -1;
                        }
                        else if (level == 3 && LevelPreName == ".<null>.info" && PreName == "files")
                        {
                            filelistb = position - 8;
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".files", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                            }
                            else return -1;
                        }
                        else
                        {
                            //其他类型列表，不属于保存范围，也不进行任何处理
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".<unknownlist>" + PreName, ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                            }
                            else return -1;
                        }
                    }

                    //字典
                    else if (PreType == 4)
                    {
                        //可能出现3个字典，最开始,info字段,files字段内
                        if (level == 1 && LevelPreName == "" && PreName == "")
                        {
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".<null>", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                                return 1;
                            }
                            else return -1;
                        }
                        else if (level == 2 && LevelPreName == ".<null>" && PreName == "info")
                        {
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".info", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                            }
                            else return -1;
                        }
                        else if (level == 4 && LevelPreName == ".<null>.info.files" && PreName == "")
                        {
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".<null>", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                            }
                            else return -1;
                        }
                        else
                        {
                            //其他类型字典，不属于保存范围，也不进行任何处理
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".<unknowndir>" + PreName, ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                            }
                            else return -1;
                        }
                    }

                    //只有四种类型，其余出错
                    else
                    {
                        return -1;
                    }
                }

                //循环次数超过
                return -1;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return -1;
            }


        }



        /// <summary>
        /// 解析种子结构，并将需要hash的部分保存，读取一下一段，返回该段内容，input为原始数据，此数据会被修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SeedReadNextSection(ref byte[] input, bool bIN, bool bSure, ref byte[] hashpart)
        {
            byte[] tmp;
            int a = 0, b = 0;
            string output = "";
            int indexbegin = 0, original = input.Length;                  //需要保留的字符串的起始，原字符串长度

            if (input.Length == 1)
            {
                if (input[0] == (byte)'e') return "SeedPhaseEnd";
                else return "SeedPhaseError";
            }
            else if ((int)input[0] >= (int)'0' && (int)input[0] <= (int)'9')            //遇到如3:abc之类数据的时候
            {
                a = Array.IndexOf(input, (byte)':');                                    //先读取数字     
                if (a < 1) return "SeedPhaseError";

                b = Utils.StrToInt(System.Text.Encoding.UTF8.GetString(input, 0, a), 0);
                if (b < 1 || original - a - b - 1 < 1)
                {
                    if (b == 0)
                    {
                        output = "SeedPhaseEMPTY";
                        indexbegin = a + b + 1;
                    }
                    else return "SeedPhaseError";         //数字不能小于1
                }
                else
                {
                    //读取数据
                    output = System.Text.Encoding.UTF8.GetString(input, a + 1, b);          //取得该段字符串值
                    if (output == "SeedPhaseD" || output == "SeedPhaseEMPTY" || output == "SeedPhaseE" || output == "SeedPhaseL") output += "ModByServer"; //防止冲突，微乎其微的可能
                    indexbegin = a + b + 1;
                }

            }
            else if (input[0] == (byte)'i')                                             //遇到整数类型的数据
            {
                a = Array.IndexOf(input, (byte)'e');
                if (a < 2 || input.Length - a - 1 < 1) return "SeedPhaseError";

                output = System.Text.Encoding.UTF8.GetString(input, 1, a - 1);          //读取数据
                indexbegin = a + 1;
            }
            else if (input[0] == (byte)'d')
            {
                output = "SeedPhaseD";
                indexbegin = 1;
            }
            else if (input[0] == (byte)'e')
            {
                output = "SeedPhaseE";
                indexbegin = 1;
            }
            else if (input[0] == (byte)'l')
            {
                output = "SeedPhaseL";
                indexbegin = 1;
            }
            else return "SeedPhaseError";

            //将需要hash的数据保存
            if (bSure || (bIN && (output == "SeedPhaseD" || output == "SeedPhaseE" || output == "SeedPhaseL" || output == "files" || output == "length" || output == "path" || output == "name" || output == "piece length" || output == "pieces")))
            //if(bIN)
            {
                tmp = new byte[hashpart.Length + indexbegin];
                Array.Copy(hashpart, tmp, hashpart.Length);
                Array.Copy(input, 0, tmp, hashpart.Length, indexbegin);
                hashpart = new byte[tmp.Length];
                Array.Copy(tmp, hashpart, tmp.Length);
            }

            //修改数组，删掉刚刚获取的数据
            tmp = new byte[original - indexbegin];
            Array.Copy(input, indexbegin, tmp, 0, tmp.Length);
            input = new byte[tmp.Length];
            Array.Copy(tmp, input, tmp.Length);                                     //将输入的数组截取后返回

            return output;

        }
        /// <summary>
        /// 检查种子文件是否正确
        /// </summary>
        /// <param name="input"></param>
        /// <param name="num">计数</param>
        /// <param name="lev">层级</param>
        /// <returns></returns>
        public static string CheckSeed(ref byte[] input, int num, int lev, ref string sectiontag, ref byte[] hashpart)
        {
            string nextsection = "", tmpstr = "";
            bool isvalue = false, bSure = false, bIN = false;
            string utcheck = "";
            for (int j = 0; j < 65535; j++)//循环一直到e结束，一开始必须去掉开头的d
            {
                if (sectiontag.Length >= 3)
                {
                    //tmpstr = sectiontag.Substring(sectiontag.Length - 3, 3);
                    //if () bSure = true;
                    if (sectiontag == ".IN.DI.LE" || sectiontag == ".IN.DI.FI.LI.DI.LE" || sectiontag == ".IN.DI.FI.LI.DI.PA.LI" || sectiontag == ".IN.DI.NA" || sectiontag == ".IN.DI.PL" || sectiontag == ".IN.DI.PI") bSure = true; //必然要保存hash的部分
                    else bSure = false;
                    tmpstr = sectiontag.Substring(0, 3);
                    if (tmpstr == ".IN") bIN = true; //可能要保存hash的部分 
                    else bIN = false;
                }
                nextsection = SeedReadNextSection(ref input, bIN, bSure, ref hashpart);
                num++;
                if (nextsection == "SeedPhaseError") return "SeedPhaseError";
                else if (nextsection == "SeedPhaseD")
                {
                    sectiontag += ".DI";
                    if (CheckSeed(ref input, 0, lev + 1, ref sectiontag, ref hashpart) == "SeedPhaseError") return "SeedPhaseError";
                }
                else if (nextsection == "SeedPhaseL")
                {
                    sectiontag += ".LI";
                    if (CheckSeed(ref input, 0, lev + 1, ref sectiontag, ref hashpart) == "SeedPhaseError") return "SeedPhaseError";
                }
                else if (nextsection == "SeedPhaseE" && lev != 0)
                {
                    if (sectiontag.Length >= 6 && sectiontag.Substring(sectiontag.Length - 3, 3) == ".PA") sectiontag = sectiontag.Substring(0, sectiontag.Length - 6);
                    else if (sectiontag.Length >= 9 && sectiontag.Substring(sectiontag.Length - 6, 6) == ".FI.LI") sectiontag = sectiontag.Substring(0, sectiontag.Length - 6);
                    else if (sectiontag.Length >= 3) sectiontag = sectiontag.Substring(0, sectiontag.Length - 3);
                    return "SeedPhaseOK";
                }
                else if (nextsection == "SeedPhaseE" && lev == 0)
                {
                    return "SeedPhaseError";
                }
                else if (nextsection == "SeedPhaseEnd" && lev != 0)
                {
                    return "SeedPhaseError";
                }
                else if (nextsection == "SeedPhaseEnd" && lev == 0)
                {
                    return "SeedPhaseOK";
                }
                else
                {
                    //处理List这个特殊情况：要么是值，要么是Dict
                    if (sectiontag.Length >= 3 && sectiontag.Substring(sectiontag.Length - 3, 3) == ".LI") isvalue = true;

                    //更改TAGS
                    if (!isvalue)
                    {
                        if (nextsection == "created by") sectiontag = ".CB";
                        else if (nextsection == "creation date") sectiontag = ".CD";
                        else if (nextsection == "info") sectiontag = ".IN";
                        else if (nextsection == "files") sectiontag += ".FI";
                        else if (nextsection == "length") sectiontag += ".LE";
                        else if (nextsection == "path") sectiontag += ".PA";
                        else if (nextsection == "name") sectiontag += ".NA";
                        else if (nextsection == "piece length") sectiontag += ".PL";
                        else if (nextsection == "pieces") sectiontag += ".PI";
                    }
                    else if (sectiontag.Length >= 3)
                    {
                        tmpstr = sectiontag.Substring(sectiontag.Length - 3, 3);
                        if (tmpstr != ".LI" && tmpstr != ".FI" && tmpstr != ".DI") sectiontag = sectiontag.Substring(0, sectiontag.Length - 3);
                    }

                    //检测是否为utf-8编码和ut种子
                    if (isvalue)
                    {

                        if (utcheck == "created by")
                        {
                            //if (nextsection.Length < 8 || nextsection.Substring(0, 8) != "uTorrent") return "SeedPhaseError";
                        }
                        if (utcheck == "encoding")
                        {
                            if (nextsection != "UTF-8") return "SeedPhaseError";
                        }
                        utcheck = "";
                    }
                    if (!isvalue)
                    {
                        if (nextsection == "created by") utcheck = "created by";
                        if (nextsection == "encoding") utcheck = "encoding";
                    }


                    //判断是否是值
                    if (!isvalue && (nextsection == "created by" || nextsection == "creation date" || nextsection == "comment" || nextsection == "announce" || nextsection == "announce-list" ||
                            nextsection == "comment.utf-8" || nextsection == "encoding" || nextsection == "length" || nextsection == "name" || nextsection == "piece length" || nextsection == "pieces" ||
                            nextsection == "publisher" || nextsection == "publisher-url" || nextsection == "publisher-url.utf-8" || nextsection == "publisher.utf-8" || nextsection == "nodes"))
                        isvalue = true;
                    else if (sectiontag.Length >= 3 && sectiontag.Substring(sectiontag.Length - 3, 3) == ".LI") isvalue = true;
                    else isvalue = false;
                }
            }
            return "SeedPhaseError";
        }


        //////////////////////////////////////////////////////////////////////////
        ///老版本：
        /// 
        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 返回值 -1出错，0结束符e，1字符串1:x，2整数i..e，3列表l..e，4字典d..e
        /// </summary>
        /// <param name="input">文件</param>
        /// <param name="position">文件的读取位置</param>
        /// <param name="SectionLong">如果为整数，此处为返回值</param>
        /// <param name="SectionString">如果为字符串，此处为返回值</param>
        /// <returns></returns>
        public static int OLDReadNext(ref byte[] input, ref int position, out long SectionLong, out string SectionString)
        {
            SectionLong = 0;
            SectionString = "";

            try
            {
                //结束符
                if (input[position] == (byte)'e')
                {
                    position++;
                    return 0;
                }

                //列表
                if (input[position] == (byte)'l')
                {
                    position++;
                    return 3;
                }

                //字典
                if (input[position] == (byte)'d')
                {
                    position++;
                    return 4;
                }

                //整数
                if (input[position] == (byte)'i')
                {
                    //搜索结束符e
                    int a = Array.IndexOf(input, (byte)'e', position + 1);
                    //截取整数字符串
                    string b = System.Text.Encoding.UTF8.GetString(input, position + 1, a - position - 1);
                    SectionLong = long.Parse(b);

                    position = a + 1;
                    return 2;
                }

                //字符串
                if ((int)input[position] >= (int)'0' && (int)input[position] <= (int)'9')
                {
                    //搜索字符串中间符号“:”
                    int a = Array.IndexOf(input, (byte)':', position + 1);
                    //截取字符串长度
                    string b = System.Text.Encoding.UTF8.GetString(input, position, a - position);
                    int c = int.Parse(b);
                    SectionString = System.Text.Encoding.UTF8.GetString(input, a + 1, c);
                    SectionLong = c;

                    position = a + c + 1;
                    return 1;
                }

                //其他情况：出错
                return -1;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return -1;
            }
        }

        public static int OLDSaveSeed(byte[] input, PTSeedinfo seedinfo)
        {
            return SaveSeed(input, ref seedinfo, "");
        }


        public static int OLDSaveSeed(byte[] input, PTSeedinfo seedinfo, string path)
        {
            string backmessage = "";
            int pos = 0;

            //文件列表段和hash段的位置
            int filelistb = 0;
            int fileliste = 0;
            int pieceb = 0;
            int piecee = 0;

            DataTable filelist = new DataTable();
            filelist.TableName = "seedfile";
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "seedid";
            filelist.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "filename";
            filelist.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Decimal");
            column.ColumnName = "filesize";
            filelist.Columns.Add(column);

            //任何数据初始化初始化放在递归调用之前
            seedinfo.SingleFile = false;

            try
            {
                if (ReadSeed(ref input, ref pos, 1, "", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                {
                    byte[] hashpart;

                    if (seedinfo.SingleFile)
                    {
                        //单文件，只读取pieces部分，其他部分生成
                        string part2s = string.Format("6:lengthi{0}e4:name{1}:{2}12:piece lengthi{3}e6:pieces{4}:", seedinfo.FileSize, System.Text.Encoding.UTF8.GetBytes(seedinfo.FolderName).Length, seedinfo.FolderName, seedinfo.PieceLength, piecee - pieceb + 1);
                        byte[] part2 = System.Text.Encoding.UTF8.GetBytes(part2s);

                        hashpart = new byte[1 + part2.Length + (piecee - pieceb + 1) + 1];

                        hashpart[0] = (byte)'d';
                        hashpart[hashpart.Length - 1] = (byte)'e';
                        Array.Copy(part2, 0, hashpart, 1, part2.Length);
                        Array.Copy(input, pieceb, hashpart, 1 + part2.Length, piecee - pieceb + 1);
                    }
                    else
                    {
                        //多文件，读取files和pieces部分，files部分结构不变
                        //byte[] part1 = new byte[fileliste - filelistb + 1];
                        string part2s = string.Format("4:name{0}:{1}12:piece lengthi{2}e6:pieces{3}:", System.Text.Encoding.UTF8.GetBytes(seedinfo.FolderName).Length, seedinfo.FolderName, seedinfo.PieceLength, piecee - pieceb + 1);
                        byte[] part2 = System.Text.Encoding.UTF8.GetBytes(part2s);
                        //byte[] part3 = new byte[piecee - pieceb + 1];

                        hashpart = new byte[1 + (fileliste - filelistb + 1) + part2.Length + (piecee - pieceb + 1) + 1];

                        hashpart[0] = (byte)'d';
                        hashpart[hashpart.Length - 1] = (byte)'e';
                        Array.Copy(input, filelistb, hashpart, 1, fileliste - filelistb + 1);
                        Array.Copy(part2, 0, hashpart, fileliste - filelistb + 2, part2.Length);
                        Array.Copy(input, pieceb, hashpart, 1 + (fileliste - filelistb + 1) + part2.Length, piecee - pieceb + 1);
                    }

                    //统一添加private项目和随机码
                    byte[] tmp = new byte[hashpart.Length + 60];
                    Array.Copy(hashpart, tmp, hashpart.Length - 1);
                    //input = new byte[57];
                    input = System.Text.Encoding.UTF8.GetBytes("7:privatei1e6:source37:FGBT-" + PTTools.GetRandomString(32) + "e");
                    Array.Copy(input, 0, tmp, hashpart.Length - 1, 61);
                    hashpart = new byte[tmp.Length];
                    Array.Copy(tmp, hashpart, tmp.Length);

                    seedinfo.InfoHash = PTTools.Byte2HEX(new SHA1CryptoServiceProvider().ComputeHash(hashpart));
                    if (seedinfo.InfoHash.Length != 40) return -2;


                    if (path == "") seedinfo.SeedId = PTSeeds.CreateSeed(seedinfo);  //在数据库中创建种子信息，如果不是修改种子的话
                    if (seedinfo.SeedId < 0) return -5;                     //创建种子失败

                    if (path != "")                                        //清理已经存在的文件列表
                    {
                        PrivateBT.DeleteSeedFileInfo(seedinfo.SeedId);
                        seedinfo.FileCount = 0;
                        seedinfo.FileSize = 0;
                    }


                    //计算总文件数和文件大小，插入种子文件列表信息
                    seedinfo.FileSize = 0;
                    seedinfo.FileCount = filelist.Rows.Count;
                    foreach (DataRow dr in filelist.Rows)
                    {
                        dr["seedid"] = seedinfo.SeedId;
                        seedinfo.FileSize += Decimal.Parse(dr["filesize"].ToString());
                    }
                    PrivateBT.InsertSeedFileList(filelist);


                    //完善种子文件，准备保存
                    string headinfo = "";
                    if (seedinfo.CreatedBy.Length > 0) headinfo += "10:created by" + seedinfo.CreatedBy.Length.ToString() + ":" + seedinfo.CreatedBy;
                    headinfo += "13:creation datei" + PTTools.Time2Int(seedinfo.CreatedDate).ToString() + "e8:encoding5:UTF-84:info";
                    byte[] headbyte = System.Text.Encoding.UTF8.GetBytes(headinfo);
                    input = new byte[headbyte.Length + hashpart.Length + 1];
                    Array.Copy(headbyte, input, headbyte.Length);
                    Array.Copy(hashpart, 0, input, headbyte.Length, hashpart.Length);
                    input[headbyte.Length + hashpart.Length] = (byte)'e';


                    //保存文件
                    DateTime savetime = DateTime.Now;
                    string savedir = "D:\\torrent\\" + savetime.ToString("yyyy") + "\\" + savetime.ToString("MM") + "\\";
                    if (path == "")
                    {
                        savedir = "D:\\torrent\\" + savetime.ToString("yyyy") + "\\" + savetime.ToString("MM") + "\\";
                        if (!Directory.Exists(savedir)) Utils.CreateDir(savedir);      //检查目录是否存在
                        if (!Directory.Exists(savedir))
                        {
                            //无法创建目录，清理记录
                            PrivateBT.DeleteSeedFileInfo(seedinfo.SeedId);
                            PTSeeds.UpdateSeedStatus(seedinfo.SeedId, -1);
                            return -4;
                        }
                        savedir += seedinfo.SeedId + ".torrent";
                        try
                        {
                            FileStream fs = new FileStream(savedir, FileMode.Create);
                            fs.Write(input, 0, input.Length);
                            fs.Flush();
                            fs.Close();
                        }
                        catch (System.Exception ex)
                        {
                            //保存文件出现问题，清理掉所有已经存在的记录
                            ex.ToString();
                            PrivateBT.DeleteSeedFileInfo(seedinfo.SeedId);
                            PTSeeds.UpdateSeedStatus(seedinfo.SeedId, -1);
                            return -3;
                        }
                    }
                    else
                    {
                        savedir = path;
                        if (path.Length > 8) path = path.Substring(0, path.Length - 8);
                        if (path.LastIndexOf("\\") < 0) return -4;
                        path = path.Substring(0, path.LastIndexOf("\\"));
                        if (!Directory.Exists(path))
                        {
                            //无法创建目录，清理记录
                            //DeleteSeedFileInfo(seedinfo.SeedId);
                            //DeleteSeed(seedinfo.SeedId);
                            return -4;
                        }
                        //savedir += seedinfo.SeedId + "." + savetime.ToString("u").Replace(":", "").Replace(" ", "") +  ".torrent";
                        try
                        {
                            File.Delete(savedir); //删掉原来的种子文件
                        }
                        catch (System.Exception ex)
                        {
                            //可能是文件不存在
                            ex.ToString();
                        }
                        try
                        {
                            FileStream fs = new FileStream(savedir, FileMode.Create);
                            fs.Write(input, 0, input.Length);
                            fs.Flush();
                            fs.Close();
                        }
                        catch (System.Exception ex)
                        {
                            //保存文件出现问题，清理掉所有已经存在的记录,但是不能删掉主题ID和种子ID
                            ex.ToString();
                            PrivateBT.DeleteSeedFileInfo(seedinfo.SeedId);
                            //DeleteSeed(seedinfo.SeedId);
                            return -3;
                        }
                    }


                    //文件保存正常，更新数据库并返回种子id
                    seedinfo.Path = savedir;
                    seedinfo.Status = 2;
                    seedinfo.UploadRatio = PTTools.GetUploadRatio(seedinfo.FileSize);
                    //if (PTTools.GetDownloadRatio(seedinfo.FileSize) < seedinfo.DownloadRatio) seedinfo.DownloadRatio = PTTools.GetDownloadRatio(seedinfo.FileSize);
                    PTSeeds.UpdateSeedEditWithSeed(seedinfo);
                    return seedinfo.SeedId;


                }
                else return -1;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return -99;
            }
        }


        public static int OLDReadSeed(ref byte[] input, ref int position, int level, string LevelPreName, ref PTSeedinfo seedinfo, out string MessageBack, ref DataTable filelist, ref int filelistb, ref int fileliste, ref int pieceb, ref int piecee)
        {
            int i = 0;
            int PreType = 0;
            string PreName = "";
            long SectionLong = 0;
            string SectionString = "";
            MessageBack = "";

            //文件列表处理需要
            long tmpFileSize = 0;
            string tmpFilePath = "";

            //函数中不应对seedinfo数值进行初始化，否则会被递归调用修改！！！递归调用切记！！
            //seedinfo.SingleFile = false; 【错误】

            try
            {
                for (i = 0; i < 65535; i++)
                {
                    PreType = ReadNext(ref input, ref position, out SectionLong, out SectionString);

                    //出错
                    if (PreType < 0) return -1;

                    //结束符e
                    else if (PreType == 0)
                    {
                        //递归调用层，返回
                        if (level > 2)
                        {
                            //第五层结束时，增加文件列表信息
                            if (level == 5 && LevelPreName == ".<null>.info.files.<null>")
                            {
                                DataRow dr = filelist.NewRow();
                                dr["filename"] = tmpFilePath;
                                dr["filesize"] = (Decimal)tmpFileSize;
                                filelist.Rows.Add(dr);
                            }
                            else if (level == 4 && LevelPreName == ".<null>.info.files")
                            {
                                fileliste = position - 1;
                            }

                            return i;
                        }
                        else
                        {
                            //第二层，检查位置是否已经移动到文件末尾
                            if (input.Length == position) return 1;
                            else return -1;
                        }
                    }

                    //字符串
                    else if (PreType == 1)
                    {

                        if (level == 2 && LevelPreName == ".<null>")
                        {
                            //第二层，可能包括 announce,announce-list,created by（字符串处理）,encoding, creation date（整数处理）等
                            //项目名
                            if (PreName == "") PreName = SectionString;
                            //数值
                            else
                            {
                                //创建者
                                if (PreName == "created by") seedinfo.CreatedBy = Utils.HtmlEncode(SectionString);
                                PreName = "";
                            }
                        }
                        else if (level == 3 && LevelPreName == ".<null>.info")
                        {
                            //第三层，可能包括files,name（字符串处理）,piece length,pieces,length（单文件情况，整数处理）等
                            //项目名
                            if (PreName == "")
                            {
                                PreName = SectionString;
                                //如果出现length，则为单文件
                                if (PreName == "length")
                                {
                                    seedinfo.SingleFile = true;
                                }
                            }
                            else
                            {
                                //数值
                                //创建者，其他字符串类型数值均不予处理
                                if (PreName == "name")
                                {
                                    if (seedinfo.SingleFile && seedinfo.FileSize!= 0 && filelist.Rows.Count < 1)
                                    {
                                        //单个文件的种子，插入文件列表的操作<><><><><>
                                        DataRow dr = filelist.NewRow();
                                        dr["filename"] = SectionString;
                                        dr["filesize"] = seedinfo.FileSize;
                                        filelist.Rows.Add(dr);
                                    }

                                    seedinfo.FolderName = SectionString;
                                }
                                else if (PreName == "pieces")
                                {
                                    piecee = position - 1;
                                    pieceb = position - (int)SectionLong;
                                }
                                PreName = "";
                            }
                        }
                        else if (level == 5 && LevelPreName == ".<null>.info.files.<null>")
                        {
                            //第五层，文件信息层，可能包括length（整数处理）,path等
                            //项目名
                            if (PreName == "") PreName = SectionString;
                            //数值
                            else
                            {
                                //暂时允许...此处不应出现字符串类型的数值
                                PreName = "";
                                //return -1;
                            }
                        }
                        else if (level == 6 && LevelPreName == ".<null>.info.files.<null>.path")
                        {
                            //第六层，文件路径层，可能包括 文件路径列表
                            MessageBack += "/" + SectionString;
                        }

                        else
                        {
                            //暂时允许...其他情况不应该出现//return -1;
                            if (PreName == "") PreName = SectionString;
                            else
                            {
                                PreName = "";
                            }
                        }
                    }

                    //整数
                    else if (PreType == 2)
                    {
                        if (PreName == "")
                        {
                            //不应出现，所有整数类型都必须有项目名
                            return -1;
                        }
                        else
                        {
                            if (level == 2 && LevelPreName == ".<null>")
                            {
                                if (PreName == "creation date") seedinfo.CreatedDate = PTTools.Int2Time((int)SectionLong);
                                //第二层允许出现其他整数类型，不作处理
                            }
                            else if (level == 3 && LevelPreName == ".<null>.info")
                            {
                                if (PreName == "piece length") seedinfo.PieceLength = (int)SectionLong;
                                if (PreName == "length")
                                {
                                    //单个文件的情况
                                    seedinfo.SingleFile = true;
                                    seedinfo.FileSize = (Decimal)SectionLong;
                                    if (seedinfo.FolderName != "" && filelist.Rows.Count < 1)
                                    {
                                        //单个文件的种子，插入文件列表的操作<><><><><>
                                        DataRow dr = filelist.NewRow();
                                        dr["filename"] = seedinfo.FolderName;
                                        dr["filesize"] = (Decimal)SectionLong;
                                        filelist.Rows.Add(dr);
                                    }
                                }

                                //第三层允许出现其他整数类型，不作处理
                            }
                            else if (level == 5 && LevelPreName == ".<null>.info.files.<null>")
                            {
                                //第五层，文件信息层，获取文件大小
                                if (PreName == "length") tmpFileSize = SectionLong;
                                else
                                {
                                    //文件信息层只允许length出现
                                    //return -1;
                                }
                            }
                            else
                            {
                                //不应该出现的情况，只允许第二、三、五层出现整数类型
                                //return -1;
                            }
                            PreName = "";
                        }

                    }

                    //列表
                    else if (PreType == 3)
                    {
                        //已知的列表项：announce-list,announce-list内列表,files,path，只需要对path和files进行处理
                        if (level == 5 && LevelPreName == ".<null>.info.files.<null>" && PreName == "path")
                        {
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".path", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                //返回的文件路径
                                tmpFilePath = backmessage;
                                PreName = "";
                            }
                            else return -1;
                        }
                        else if (level == 3 && LevelPreName == ".<null>.info" && PreName == "files")
                        {
                            filelistb = position - 8;
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".files", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                            }
                            else return -1;
                        }
                        else
                        {
                            //其他类型列表，不属于保存范围，也不进行任何处理
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".<unknownlist>" + PreName, ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                            }
                            else return -1;
                        }
                    }

                    //字典
                    else if (PreType == 4)
                    {
                        //可能出现3个字典，最开始,info字段,files字段内
                        if (level == 1 && LevelPreName == "" && PreName == "")
                        {
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".<null>", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                                return 1;
                            }
                            else return -1;
                        }
                        else if (level == 2 && LevelPreName == ".<null>" && PreName == "info")
                        {
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".info", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                            }
                            else return -1;
                        }
                        else if (level == 4 && LevelPreName == ".<null>.info.files" && PreName == "")
                        {
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".<null>", ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                            }
                            else return -1;
                        }
                        else
                        {
                            //其他类型字典，不属于保存范围，也不进行任何处理
                            string backmessage = "";
                            if (ReadSeed(ref input, ref position, level + 1, LevelPreName + ".<unknowndir>" + PreName, ref seedinfo, out backmessage, ref filelist, ref filelistb, ref fileliste, ref pieceb, ref piecee) >= 0)
                            {
                                PreName = "";
                            }
                            else return -1;
                        }
                    }

                    //只有四种类型，其余出错
                    else
                    {
                        return -1;
                    }
                }

                //循环次数超过
                return -1;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return -1;
            }

            
        }



        /// <summary>
        /// 解析种子结构，并将需要hash的部分保存，读取一下一段，返回该段内容，input为原始数据，此数据会被修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string OLDSeedReadNextSection(ref byte[] input, bool bIN, bool bSure, ref byte[] hashpart)
        {
            byte[] tmp;
            int a = 0, b = 0;
            string output = "";
            int indexbegin = 0, original = input.Length;                  //需要保留的字符串的起始，原字符串长度

            if (input.Length == 1)
            {
                if (input[0] == (byte)'e') return "SeedPhaseEnd";
                else return "SeedPhaseError";
            }
            else if ((int)input[0] >= (int)'0' && (int)input[0] <= (int)'9')            //遇到如3:abc之类数据的时候
            {
                a = Array.IndexOf(input, (byte)':');                                    //先读取数字     
                if (a < 1) return "SeedPhaseError";

                b = Utils.StrToInt(System.Text.Encoding.UTF8.GetString(input, 0, a), 0);
                if (b < 1 || original - a - b - 1 < 1)
                {
                    if (b == 0)
                    {
                        output = "SeedPhaseEMPTY";
                        indexbegin = a + b + 1;
                    }
                    else return "SeedPhaseError";         //数字不能小于1
                }
                else
                {
                    //读取数据
                    output = System.Text.Encoding.UTF8.GetString(input, a + 1, b);          //取得该段字符串值
                    if (output == "SeedPhaseD" || output == "SeedPhaseEMPTY" || output == "SeedPhaseE" || output == "SeedPhaseL") output += "ModByServer"; //防止冲突，微乎其微的可能
                    indexbegin = a + b + 1;
                }

            }
            else if (input[0] == (byte)'i')                                             //遇到整数类型的数据
            {
                a = Array.IndexOf(input, (byte)'e');
                if (a < 2 || input.Length - a - 1 < 1) return "SeedPhaseError";

                output = System.Text.Encoding.UTF8.GetString(input, 1, a - 1);          //读取数据
                indexbegin = a + 1;
            }
            else if (input[0] == (byte)'d')
            {
                output = "SeedPhaseD";
                indexbegin = 1;
            }
            else if (input[0] == (byte)'e')
            {
                output = "SeedPhaseE";
                indexbegin = 1;
            }
            else if (input[0] == (byte)'l')
            {
                output = "SeedPhaseL";
                indexbegin = 1;
            }
            else return "SeedPhaseError";

            //将需要hash的数据保存
            if (bSure || (bIN && (output == "SeedPhaseD" || output == "SeedPhaseE" || output == "SeedPhaseL" || output == "files" || output == "length" || output == "path" || output == "name" || output == "piece length" || output == "pieces")))
            //if(bIN)
            {
                tmp = new byte[hashpart.Length + indexbegin];
                Array.Copy(hashpart, tmp, hashpart.Length);
                Array.Copy(input, 0, tmp, hashpart.Length, indexbegin);
                hashpart = new byte[tmp.Length];
                Array.Copy(tmp, hashpart, tmp.Length);
            }

            //修改数组，删掉刚刚获取的数据
            tmp = new byte[original - indexbegin];
            Array.Copy(input, indexbegin, tmp, 0, tmp.Length);
            input = new byte[tmp.Length];
            Array.Copy(tmp, input, tmp.Length);                                     //将输入的数组截取后返回

            return output;

        }
        /// <summary>
        /// 检查种子文件是否正确
        /// </summary>
        /// <param name="input"></param>
        /// <param name="num">计数</param>
        /// <param name="lev">层级</param>
        /// <returns></returns>
        public static string OLDCheckSeed(ref byte[] input, int num, int lev, ref string sectiontag, ref byte[] hashpart)
        {
            string nextsection = "", tmpstr = "";
            bool isvalue = false, bSure = false, bIN = false;
            string utcheck = "";
            for (int j = 0; j < 65535; j++)//循环一直到e结束，一开始必须去掉开头的d
            {
                if (sectiontag.Length >= 3)
                {
                    //tmpstr = sectiontag.Substring(sectiontag.Length - 3, 3);
                    //if () bSure = true;
                    if (sectiontag == ".IN.DI.LE" || sectiontag == ".IN.DI.FI.LI.DI.LE" || sectiontag == ".IN.DI.FI.LI.DI.PA.LI" || sectiontag == ".IN.DI.NA" || sectiontag == ".IN.DI.PL" || sectiontag == ".IN.DI.PI") bSure = true; //必然要保存hash的部分
                    else bSure = false;
                    tmpstr = sectiontag.Substring(0, 3);
                    if (tmpstr == ".IN") bIN = true; //可能要保存hash的部分 
                    else bIN = false;
                }
                nextsection = SeedReadNextSection(ref input, bIN, bSure, ref hashpart);
                num++;
                if (nextsection == "SeedPhaseError") return "SeedPhaseError";
                else if (nextsection == "SeedPhaseD")
                {
                    sectiontag += ".DI";
                    if (CheckSeed(ref input, 0, lev + 1, ref sectiontag, ref hashpart) == "SeedPhaseError") return "SeedPhaseError";
                }
                else if (nextsection == "SeedPhaseL")
                {
                    sectiontag += ".LI";
                    if (CheckSeed(ref input, 0, lev + 1, ref sectiontag, ref hashpart) == "SeedPhaseError") return "SeedPhaseError";
                }
                else if (nextsection == "SeedPhaseE" && lev != 0)
                {
                    if (sectiontag.Length >= 6 && sectiontag.Substring(sectiontag.Length - 3, 3) == ".PA") sectiontag = sectiontag.Substring(0, sectiontag.Length - 6);
                    else if (sectiontag.Length >= 9 && sectiontag.Substring(sectiontag.Length - 6, 6) == ".FI.LI") sectiontag = sectiontag.Substring(0, sectiontag.Length - 6);
                    else if (sectiontag.Length >= 3) sectiontag = sectiontag.Substring(0, sectiontag.Length - 3);
                    return "SeedPhaseOK";
                }
                else if (nextsection == "SeedPhaseE" && lev == 0)
                {
                    return "SeedPhaseError";
                }
                else if (nextsection == "SeedPhaseEnd" && lev != 0)
                {
                    return "SeedPhaseError";
                }
                else if (nextsection == "SeedPhaseEnd" && lev == 0)
                {
                    return "SeedPhaseOK";
                }
                else
                {
                    //处理List这个特殊情况：要么是值，要么是Dict
                    if (sectiontag.Length >= 3 && sectiontag.Substring(sectiontag.Length - 3, 3) == ".LI") isvalue = true;

                    //更改TAGS
                    if (!isvalue)
                    {
                        if (nextsection == "created by") sectiontag = ".CB";
                        else if (nextsection == "creation date") sectiontag = ".CD";
                        else if (nextsection == "info") sectiontag = ".IN";
                        else if (nextsection == "files") sectiontag += ".FI";
                        else if (nextsection == "length") sectiontag += ".LE";
                        else if (nextsection == "path") sectiontag += ".PA";
                        else if (nextsection == "name") sectiontag += ".NA";
                        else if (nextsection == "piece length") sectiontag += ".PL";
                        else if (nextsection == "pieces") sectiontag += ".PI";
                    }
                    else if (sectiontag.Length >= 3)
                    {
                        tmpstr = sectiontag.Substring(sectiontag.Length - 3, 3);
                        if (tmpstr != ".LI" && tmpstr != ".FI" && tmpstr != ".DI") sectiontag = sectiontag.Substring(0, sectiontag.Length - 3);
                    }

                    //检测是否为utf-8编码和ut种子
                    if (isvalue)
                    {

                        if (utcheck == "created by")
                        {
                            //if (nextsection.Length < 8 || nextsection.Substring(0, 8) != "uTorrent") return "SeedPhaseError";
                        }
                        if (utcheck == "encoding")
                        {
                            if (nextsection != "UTF-8") return "SeedPhaseError";
                        }
                        utcheck = "";
                    }
                    if (!isvalue)
                    {
                        if (nextsection == "created by") utcheck = "created by";
                        if (nextsection == "encoding") utcheck = "encoding";
                    }


                    //判断是否是值
                    if (!isvalue && (nextsection == "created by" || nextsection == "creation date" || nextsection == "comment" || nextsection == "announce" || nextsection == "announce-list" ||
                            nextsection == "comment.utf-8" || nextsection == "encoding" || nextsection == "length" || nextsection == "name" || nextsection == "piece length" || nextsection == "pieces" ||
                            nextsection == "publisher" || nextsection == "publisher-url" || nextsection == "publisher-url.utf-8" || nextsection == "publisher.utf-8" || nextsection == "nodes"))
                        isvalue = true;
                    else if (sectiontag.Length >= 3 && sectiontag.Substring(sectiontag.Length - 3, 3) == ".LI") isvalue = true;
                    else isvalue = false;
                }
            }
            return "SeedPhaseError";
        }
    }
}
