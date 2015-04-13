using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Discuz.Entity
{
    /// <summary>
    /// 种子结构信息类
    /// </summary>
    public class FGBTSeedStructure
    {
        /// <summary>
        /// 是否为单文件
        /// </summary>
        public bool SigleFile = false;
        /// <summary>
        /// 种子创建时间
        /// </summary>
        public DateTime CreateDate = DateTime.MinValue;
        /// <summary>
        /// 种子创建软件
        /// </summary>
        public string CreateBy = "";
        /// <summary>
        /// 种子编码
        /// </summary>
        public string Encoding = "";
        /// <summary>
        /// 种子文件列表
        /// </summary>
        public List<FGBTSeedStructure_FileList> FileList;
        /// <summary>
        /// 种子文件夹名
        /// </summary>
        public string FolderName = "";
        /// <summary>
        /// 种子每个块的大小
        /// </summary>
        public long PieceLength = 0;
        /// <summary>
        /// 种子Piece字段byte长度
        /// </summary>
        public long PieceByteCount = 0;
        /// <summary>
        /// 种子Piece字段
        /// </summary>
        public byte[] Piece;
        /// <summary>
        /// 是否为私有种子
        /// </summary>
        public int Private = 1;
        /// <summary>
        /// 来源站
        /// </summary>
        public string Source = "";

        /// <summary>
        /// Info字段byte长度
        /// </summary>
        public long InfoByteCount = 0;
        /// <summary>
        /// Info字段
        /// </summary>
        public byte[] Info;
        /// <summary>
        /// Info字段hash
        /// </summary>
        public string InfoHash = "";
    }
    /// <summary>
    /// 种子结构信息类-子类：文件列表
    /// </summary>
    public class FGBTSeedStructure_FileList
    {
        /// <summary>
        /// 文件长度
        /// </summary>
        public long Length = 0;
        /// <summary>
        /// 文件路径列表
        /// </summary>
        public List<string> Path;
    }
}
