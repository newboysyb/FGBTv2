using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Discuz.Entity
{
    /// <summary>
    /// 种子标签信息类，存放种子的文字分类信息
    /// </summary>
    public class PT_SeedTagInfo
    {
        //公共信息存储区域
        // 

        private string INFO_TYPE = "";      //本信息类存储的种子类型
        private string cname = "";          //中文名
        private string ename = "";          //英文名
        private string imdb = "";           //imdb编号（电影独有）
        private string type = "";           //类别
        private string resolution = "";     //分辨率
        private string video = "";          //视频编码
        private string audio = "";          //音频编码  
        private string director = "";       //导演（电影独有）  
        private string actor = "";          //演员（电影独有）
        private string year = "";           //年份
        private string region = "";         //国家地区
        private string language = "";       //语言
        private string subtitle = "";       //字幕情况  
        private string rank = "";           //MPAA评级（电影独有）
        private string season = "";         //季度信息
        private string source = "";         //片源（动漫独有）
        private string subtitlegroup = "";  //字幕组（动漫独有）
        private string artist = "";         //艺术家（音乐独有）
        private string bps = "";            //码率（音乐独有）
        private string company = "";        //发行公司（音乐独有）
        private string format = "";         //文件格式
        private string brief = "";          //简介（娱乐独有）
        private string platform = "";       //平台（游戏独有）

        //属性访问区域
        //
        public string TYPE { get { return INFO_TYPE.Trim() == "" ? "" : INFO_TYPE; } set { INFO_TYPE = value; } }

        public string Movie_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Movie_ename { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Movie_imdb { get { return imdb.Trim() == "" ? "tt00000000" : imdb; } set { imdb = value; } }
        public string Movie_type { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Movie_resolution { get { return resolution.Trim() == "" ? "720P" : resolution; } set { resolution = value; } }
        public string Movie_video { get { return video.Trim() == "" ? "" : video; } set { video = value; } }
        public string Movie_audio { get { return audio.Trim() == "" ? "" : audio; } set { audio = value; } }
        public string Movie_director { get { return director.Trim() == "" ? "" : director; } set { director = value; } }
        public string Movie_actor { get { return actor.Trim() == "" ? "" : actor; } set { actor = value; } }
        public string Movie_year { get { return year.Trim() == "" ? DateTime.Now.Year.ToString() : year; } set { year = value; } }
        public string Movie_region { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Movie_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Movie_subtitle { get { return subtitle.Trim() == "" ? "暂无字幕" : subtitle; } set { subtitle = value; } }
        public string Movie_rank { get { return rank.Trim() == "" ? "R" : rank; } set { rank = value; } }

        public string Tv_region { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Tv_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Tv_ename { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Tv_season { get { return season.Trim() == "" ? "" : season; } set { season = value; } }
        public string Tv_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Tv_resolution { get { return resolution.Trim() == "" ? "." : resolution; } set { resolution = value; } }
        public string Tv_subtitle { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }

        public string Comic_region { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Comic_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Comic_ename { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Comic_season { get { return season.Trim() == "" ? "" : season; } set { season = value; } }
        public string Comic_type { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Comic_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Comic_format { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Comic_source { get { return source.Trim() == "" ? "." : source; } set { source = value; } }
        public string Comic_subtitle { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }
        public string Comic_subtitlegroup { get { return subtitlegroup.Trim() == "" ? "." : subtitlegroup; } set { subtitlegroup = value; } }
        public string Comic_year { get { return year.Trim() == "" ? "." : year; } set { year = value; } }

        public string Music_region { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Music_type { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Music_artist { get { return artist.Trim() == "" ? "." : artist; } set { artist = value; } }
        public string Music_name { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Music_year { get { return year.Trim() == "" ? "." : year; } set { year = value; } }
        public string Music_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Music_format { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Music_bps { get { return bps.Trim() == "" ? "." : bps; } set { bps = value; } }
        public string Music_company { get { return company.Trim() == "" ? "" : company; } set { company = value; } }

        public string Discovery_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Discovery_ename { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Discovery_type { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Discovery_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Discovery_format { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Discovery_resolution { get { return resolution.Trim() == "" ? "." : resolution; } set { resolution = value; } }
        public string Discovery_subtitle { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }

        public string Sport_year { get { return year.Trim() == "" ? "." : year; } set { year = value; } }
        public string Sport_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Sport_ename { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Sport_type { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Sport_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Sport_format { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Sport_resolution { get { return resolution.Trim() == "" ? "." : resolution; } set { resolution = value; } }
        public string Sport_subtitle { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }

        public string Entertainment_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Entertainment_ename { get { return ename.Trim() == "" ? "" : ename; } set { ename = value; } }
        public string Entertainment_region { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Entertainment_year { get { return year.Trim() == "" ? "." : year; } set { year = value; } }
        public string Entertainment_brief { get { return brief.Trim() == "" ? "." : brief; } set { brief = value; } }
        public string Entertainment_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Entertainment_format { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Entertainment_resolution { get { return resolution.Trim() == "" ? "." : resolution; } set { resolution = value; } }
        public string Entertainment_subtitle { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }

        public string Other_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Other_ename { get { return ename.Trim() == "" ? "" : ename; } set { ename = value; } }
        public string Other_brief { get { return brief.Trim() == "" ? "" : brief; } set { brief = value; } }
        public string Other_format { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Other_year { get { return year.Trim() == "" ? "" : year; } set { year = value; } }

        public string Game_platform { get { return platform.Trim() == "" ? "." : platform; } set { platform = value; } }
        public string Game_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Game_ename { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Game_type { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Game_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Game_format { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Game_company { get { return company.Trim() == "" ? "" : company; } set { company = value; } }
        public string Game_region { get { return region.Trim() == "" ? "" : region; } set { region = value; } }

        public string Software_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Software_ename { get { return ename.Trim() == "" ? "" : ename; } set { ename = value; } }
        public string Software_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Software_type { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Software_format { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Software_year { get { return year.Trim() == "" ? "" : year; } set { year = value; } }

        public string Staff_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Staff_ename { get { return ename.Trim() == "" ? "" : ename; } set { ename = value; } }
        public string Staff_type { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Staff_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Staff_format { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Staff_year { get { return year.Trim() == "" ? "" : year; } set { year = value; } }

        public string Video_year { get { return year.Trim() == "" ? "" : year; } set { year = value; } }
        public string Video_region { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Video_cname { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Video_ename { get { return ename.Trim() == "" ? "" : ename; } set { ename = value; } }
        public string Video_language { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Video_format { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Video_resolution { get { return resolution.Trim() == "" ? "." : resolution; } set { resolution = value; } }
        public string Video_subtitle { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }



        public string Movie_1 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Movie_2 { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Movie_3 { get { return imdb.Trim() == "" ? "tt00000000" : imdb; } set { imdb = value; } }
        public string Movie_4 { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Movie_5 { get { return resolution.Trim() == "" ? "720P" : resolution; } set { resolution = value; } }
        public string Movie_6 { get { return video.Trim() == "" ? "" : video; } set { video = value; } }
        public string Movie_7 { get { return audio.Trim() == "" ? "" : audio; } set { audio = value; } }
        public string Movie_8 { get { return director.Trim() == "" ? "" : director; } set { director = value; } }
        public string Movie_9 { get { return actor.Trim() == "" ? "" : actor; } set { actor = value; } }
        public string Movie_10 { get { return year.Trim() == "" ? DateTime.Now.Year.ToString() : year; } set { year = value; } }
        public string Movie_11 { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Movie_12 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Movie_13 { get { return subtitle.Trim() == "" ? "暂无字幕" : subtitle; } set { subtitle = value; } }
        public string Movie_14 { get { return rank.Trim() == "" ? "R" : rank; } set { rank = value; } }

        public string Tv_1 { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Tv_2 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Tv_3 { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Tv_4 { get { return season.Trim() == "" ? "" : season; } set { season = value; } }
        public string Tv_5 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Tv_6 { get { return resolution.Trim() == "" ? "." : resolution; } set { resolution = value; } }
        public string Tv_7 { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }

        public string Comic_1 { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Comic_2 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Comic_3 { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Comic_4 { get { return season.Trim() == "" ? "" : season; } set { season = value; } }
        public string Comic_5 { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Comic_6 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Comic_7 { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Comic_8 { get { return source.Trim() == "" ? "." : source; } set { source = value; } }
        public string Comic_9 { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }
        public string Comic_10 { get { return subtitlegroup.Trim() == "" ? "." : subtitlegroup; } set { subtitlegroup = value; } }
        public string Comic_11 { get { return year.Trim() == "" ? "." : year; } set { year = value; } }

        public string Music_1 { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Music_2 { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Music_3 { get { return artist.Trim() == "" ? "." : artist; } set { artist = value; } }
        public string Music_4 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Music_5 { get { return year.Trim() == "" ? "." : year; } set { year = value; } }
        public string Music_6 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Music_7 { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Music_8 { get { return bps.Trim() == "" ? "." : bps; } set { bps = value; } }
        public string Music_9 { get { return company.Trim() == "" ? "" : company; } set { company = value; } }

        public string Discovery_1 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Discovery_2 { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOIDiscovery_" : ename; } set { ename = value; } }
        public string Discovery_3 { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Discovery_4 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Discovery_5 { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Discovery_6 { get { return resolution.Trim() == "" ? "." : resolution; } set { resolution = value; } }
        public string Discovery_7 { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }

        public string Sport_1 { get { return year.Trim() == "" ? "." : year; } set { year = value; } }
        public string Sport_2 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Sport_3 { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Sport_4 { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Sport_5 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Sport_6 { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Sport_7 { get { return resolution.Trim() == "" ? "." : resolution; } set { resolution = value; } }
        public string Sport_8 { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }

        public string Entertainment_1 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Entertainment_2 { get { return ename.Trim() == "" ? "" : ename; } set { ename = value; } }
        public string Entertainment_3 { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Entertainment_4 { get { return year.Trim() == "" ? "." : year; } set { year = value; } }
        public string Entertainment_5 { get { return brief.Trim() == "" ? "." : brief; } set { brief = value; } }
        public string Entertainment_6 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Entertainment_7 { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Entertainment_8 { get { return resolution.Trim() == "" ? "." : resolution; } set { resolution = value; } }
        public string Entertainment_9 { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }

        public string Other_1 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Other_2 { get { return ename.Trim() == "" ? "" : ename; } set { ename = value; } }
        public string Other_3 { get { return brief.Trim() == "" ? "" : brief; } set { brief = value; } }
        public string Other_4 { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Other_5 { get { return year.Trim() == "" ? "" : year; } set { year = value; } }

        public string Game_1 { get { return platform.Trim() == "" ? "." : platform; } set { platform = value; } }
        public string Game_2 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Game_3 { get { return ename.Trim() == "" ? "ENGLISH_NAME_TEMP_VOID" : ename; } set { ename = value; } }
        public string Game_4 { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Game_5 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Game_6 { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Game_7 { get { return company.Trim() == "" ? "" : company; } set { company = value; } }
        public string Game_8 { get { return region.Trim() == "" ? "" : region; } set { region = value; } }

        public string Software_1 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Software_2 { get { return ename.Trim() == "" ? "" : ename; } set { ename = value; } }
        public string Software_3 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Software_4 { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Software_5 { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Software_6 { get { return year.Trim() == "" ? "" : year; } set { year = value; } }

        public string Staff_1 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Staff_2 { get { return ename.Trim() == "" ? "" : ename; } set { ename = value; } }
        public string Staff_3 { get { return type.Trim() == "" ? "." : type; } set { type = value; } }
        public string Staff_4 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Staff_5 { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Staff_6 { get { return year.Trim() == "" ? "" : year; } set { year = value; } }

        public string Video_1 { get { return year.Trim() == "" ? "" : year; } set { year = value; } }
        public string Video_2 { get { return region.Trim() == "" ? "." : region; } set { region = value; } }
        public string Video_3 { get { return cname.Trim() == "" ? "暂无中文名信息" : cname; } set { cname = value; } }
        public string Video_4 { get { return ename.Trim() == "" ? "" : ename; } set { ename = value; } }
        public string Video_5 { get { return language.Trim() == "" ? "." : language; } set { language = value; } }
        public string Video_6 { get { return format.Trim() == "" ? "." : format; } set { format = value; } }
        public string Video_7 { get { return resolution.Trim() == "" ? "." : resolution; } set { resolution = value; } }
        public string Video_8 { get { return subtitle.Trim() == "" ? "." : subtitle; } set { subtitle = value; } }
    }
}
