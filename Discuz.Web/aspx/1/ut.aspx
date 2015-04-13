<STYLE type=text/css media=screen>
#toc H2 {
	DISPLAY: inline
}
.toctoggle {
	MARGIN-LEFT: 0.4em
}
#firon {
	MARGIN-LEFT: 0.3em
}
.tocline {
	BORDER-RIGHT: #eaead1 2px solid; PADDING-RIGHT: 1em; BORDER-TOP: #eaead1 2px solid; PADDING-LEFT: 1em; BACKGROUND: #f6f6ea; PADDING-BOTTOM: 0.6em; MARGIN: 1.2em 0px 0.3em; BORDER-LEFT: #eaead1 2px solid; PADDING-TOP: 0.6em; BORDER-BOTTOM: #eaead1 2px solid
}
.tocline A {
	FONT-SIZE: 1em; COLOR: #333
}
.tocline A:hover {
	COLOR: #555; TEXT-DECORATION: underline
}
.tocindent {
	PADDING-RIGHT: 2.2em; PADDING-LEFT: 2.2em; BACKGROUND: #fff; PADDING-BOTTOM: 1em; PADDING-TOP: 1em
}
.tocindent A {
	PADDING-RIGHT: 0px; DISPLAY: block; PADDING-LEFT: 16px; MARGIN-BOTTOM: -0.4em; PADDING-BOTTOM: 0.4em; COLOR: #235f23; PADDING-TOP: 0px; BORDER-BOTTOM: #f5f5f5 1px solid
}
.tocindent A:hover {
	COLOR: #444
}
#innercontent H2 {
	DISPLAY: block; MARGIN: 1em 0px 0px; BORDER-BOTTOM: #ccc 2px solid
}
#innercontent H3 {
	MARGIN: 1.1em 0px 0px
}
#innercontent P {
	MARGIN-TOP: 1em; FONT: 9pt Tahoma; COLOR: #000; LETTER-SPACING: 0px
}
#innercontent UL {
	FONT: 9pt Tahoma; COLOR: #000
}
#innercontent A {
	TEXT-DECORATION: underline
}
IMG {
	BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px
}
</STYLE>
<!-- Some IE Fixes, hasLayout declarations, etc --><!--[if IE]>
<STYLE>DIV#innercontent {
	HEIGHT: 1%
}
DIV#top {
	HEIGHT: 1%
}
DIV#hype {
	HEIGHT: 1%
}
DIV#navi UL {
	HEIGHT: 1%
}
DIV#navi {
	HEIGHT: 1%
}
DIV#footer {
	POSITION: relative; TOP: -1px
}
.tocindent {
	POSITION: relative
}
</STYLE>
<![endif]--><!-- Detect avail. width, adjust accordingly -->
<SCRIPT type=text/javascript>
		<!--
		var x;
		if (self.innerHeight) { x = self.innerWidth; }
		else if (document.documentElement && document.documentElement.clientHeight)
		{ x = document.documentElement.clientWidth; }
		if (x >= 990) { document.write('<style>div#wrapper { width: 67em; }</style>'); }
		else { document.write('<style>div#wrapper { width: 100%; } div#content { margin: 0; }</style>'); }
		//-->
	</SCRIPT>
<!-- IE5.x only fixes --><!--[if IE 5]><script type="text/javascript">
	if (document.body.clientWidth >= 990)
	{ document.write('<style>div#wrapper { width: 67em; } div#content { margin: 0 1em; }</style>'); }
	else { document.write('<style>div#wrapper { width: 100%; } div#content { margin: 0; }</style>'); }
	document.write('<style>body, p, h3 { margin: 0; } div#innercontent div#screenshots { width: 710px; }</style>');
</script><![endif]-->
<DIV id=wrapper>
<DIV id=outerwrapper>
<DIV id=innerwrapper>
<DIV id=content>
<DIV class=clearfix id=hype>
<SCRIPT src="images/faqtoc.js" type=text/javascript></SCRIPT>
<!-- Last edited: 7/14/2006 1:14PM -->
<DIV id=toc>
<H2>目录</H2>
<SCRIPT type=text/javascript>showTocToggle("显示","隐藏");</SCRIPT>

</DIV>
<P id=firon>本中文帮助由 风之痕 和 lemon 共同维护, 整理翻译自官方网站 FAQ, 最后更新于 2007.07.17. 转载请保留此信息, 谢谢!</P>
<DIV id=tocinside>
<DIV class=tocline><A href="#Incompatible_software">1. 兼容性</A><BR></DIV>
<DIV class=tocindent>
<P><A 
href="#Help.21_My.C2.B5Torrent_process_is_using_a_ton_of_memory.21">1.1 
为什么我的 µTorrent 进程占用了非常大的内存? (可能由 NVIDIA 防火墙引起)</A><BR><A 
href="#My.C2.B5Torrent_keeps_freezing_on_certain_trackers">1.2 
为什么我的 µTorrent 在与服务器通讯时假死且提示 "向一个无法连接的网络尝试了一个套接字操作." (可能由 BitDefender 或其它程序引起)</A><BR><A 
href="#My_.C2.B5Torrent_window_doesnt_update_or_refresh">1.3 
为什么我的 µTorrent 窗口不自动更新或刷新? (可能由 Stardock ObjectDock 引起)</A><BR><A 
href="#I_get_Error_Access_Denied_and_.C2.B5Torrent_halts_the_torrent">1.4 
我在使用 µTorrent 时遇到 "错误: 访问被拒绝" 或 "错误: 进程无法访问文件, 该文件正被另一进程使用." 等类似的提示然后 µTorrent 自动挂起任务的情况. 
(Google/MSN Desktop, 某些反病毒软件, WinZIP 快速查找功能, 资源管理器开启缩略图及其它原因)</A><BR><A 
href="#My.C2.B5Torrent_freezes_locks_up_sometimes_and_or_uses_100_CPU">1.5 
为什么我的 µTorrent 有时假死或 CPU 使用达到 100% 从而减慢了我的计算机的运行速度? (可能由 Avast!, Spyware Doctor 5 及其它程序引起)</A><BR><A 
href="#Kerio_Sunbelt_uses_100_CPU_when_running_.C2.B5Torrent">1.6 
为什么 Kerio 个人防火墙程序或 Sunbelt 个人防火墙程序在运行 µTorrent 的时候 CPU 占用达到 100% 或断开了网络连接?</A><BR><B><A 
href="#Special_note_for_users_with_Linksys_WRT54G_GL_GS_routers">1.7 
使用 Linksys WRT54G/GL/GS 路由器的用户请注意, 这些路由器在你运行 P2P 程序时可能会出现一些问题 (点此查看解决方法)</A></B><BR><A 
href="#Norton_keeps_prompting_whether_to_permit_or_block_.C2.B5Torrent.21">1.8 
为什么 Norton 反病毒程序总是让我选择是允许还是阻止 µTorrent?</A><BR><A 
href="#I_get_a_socket_buffer_space_error_and_the_client_halts">1.9 
我遇到下列提示 "无法执行套接字操作, 系统缓冲空间不足或队列已满" 然后 µTorrent 失去响应. (可能出现在安装了 Norton GoBack 的 Windows 2000/XP/2003 系统上)</A><BR><A 
href="#Modems_routers_that_are_known_to_have_problems_with_P2P">1.10 
目前已知的与 P2P 程序存在兼容性的调制解调器或路由器名单 (来自 Azureus, 同样适用于 µTorrent)</A><BR><A 
href="#I_get_tons_of_hashfails_on_my_torrents_and_the_torrent_never_finishes">1.11 
我在进行下载任务时发现出现大量校验失败的情况, 并且该任务始终无法完成: 它始终停在 99.9%! (可能与 D-link 或其它某些开启 DMZ 模式的路由器有关)</A><BR><A 
href="#I_get_very_high_CPU_use_when_running_.C2.B5Torrent.21">1.12 
为什么 µTorrent 运行时 CPU 使用达到 100%? (可能与 McAfee 防火墙, SpamPal, Norman 个人防火墙或其它原因有关)</A><BR><A 
href="#.C2.B5Torrent_crashes.21">1.13 µTorrent 
崩溃了... (可能与 V-Com 系统工具套装 &amp; V-Com Fixit Pro, Norman 个人防火墙及 McAfee 防火墙有关)</A><BR><A 
href="#I_get_blank_dialogs_in_various_places">1.14 
我在使用 µTorrent 时遇到一些空白对话框或某些位置缺少文字. (可能与 McAfee 反病毒, Norton 反病毒程序有关)</A><BR></P></DIV>
<DIV class=tocline><A href="#General">2 
常规</A><BR></DIV>
<DIV class=tocindent>
<P><A 
href="#What_are_.C2.B5Torrent.27s_system_requirements.3F">2.1 
µTorrent 的系统需求是什么?</A><BR><A 
href="#Is_.C2.B5Torrent_open_source.3F">2.2  
µTorrent 是开源软件吗?</A><BR><A 
href="#Is_there_a_Linux_or_Mac_version.3F">2.3  
µTorrent 有 Linux 或 Mac 版本吗?</A><BR><A 
href="#How_do_I_make_.C2.B5Torrent_prioritize_the_first_and_last_piece.3F">2.4 
如何使 µTorrent 自动调整第一块和最后一块的优先级?</A><BR><A 
href="#How_do_I_make_.C2.B5Torrent_allocate_all_the_files_when_I_start_the_torrent.3F">2.5 
如何使 µTorrent 在开始任务前为文件自动分配空间?</A><BR><A 
href="#How_do_I_modify_the_disk_cache_options.3F">2.6 
如何修改磁盘缓冲参数?</A><BR><A 
href="#How_do_I_change_the_default_remove_action.3F">2.7 
如何更改默认删除操作?</A><BR><A 
href="#How_do_I_make_.C2.B5Torrent_delete_files_to_the_recycle_bin.3F">2.8 
如何使 µTorrent 删除文件到回收站?</A><BR><A 
href="#Is_it_possible_to_make_uTorrent_load_a_torrent_from_the_commandline.3F">2.9 
µTorrent 支持从命令行载入种子文件吗?</A><BR><A 
href="#How_can_I_add_other_columns_of_info.3F">2.10 
如何添加其他信息栏? 可否按多个标准对其进行排序操作?</A><BR><A 
href="#How_can_I_quickly_change_the_upload_and_download_caps.3F">2.11 
如何快速更改上传和下载速度限制?</A><BR><A 
href="#How_can_I_tell_if_a_peer_is_an_incoming_or_outgoing_connection.3F">2.12 
如何判断一个用户是连入连接还是连出连接?</A><BR><A 
href="#How_can_I_make_.C2.B5Torrent_stop_seeding_at_a_specific_share_ratio.3F">2.13 
如何使 µTorrent 在达到指定的分享率后停止做种?</A><BR><A 
href="#How_can_I_make_.C2.B5Torrent_auto-load_torrents_from_a_specified_folder.3F">2.14 
如何使 µTorrent 从指定的文件夹中自动载入种子?</A><BR><A 
href="#How_can_I_rename_a_torrent_folder.3F">2.15 
如何重命名任务的保存文件夹?</A><BR><A 
href="#How_can_I_rename_a_torrent_in_the_main_listview.3F">2.16 
如何重命名任务列表中的任务名称?</A><BR><A 
href="#How_can_I_make_.C2.B5Torrent_append_a_ut_extension_to_incomplete_files.3F">2.17 
如何使 µTorrent 附加扩展名 !ut 到未完成下载的文件名之后以便区别?</A><BR><A 
href="#How_can_I_make_.C2.B5Torrent_start_minimized.3F">2.18 
如何使 µTorrent 以最小化方式启动?</A><BR><A 
href="#How_can_I_make_.C2.B5Torrent_start_in_bosskey_mode.3F">2.19 
如何使 µTorrent 以隐藏模式启动?</A><BR><A 
href="#How_can_I_make_.C2.B5Torrent_go_into_a_seed-only_mode_on_all_torrents.3F">2.20 
如何使 µTorrent 对所有任务进入仅做种模式?</A><BR><A 
href="#Can_.C2.B5Torrent_automatically_move_files_when_the_torrent_finishes.3F">2.21 
µTorrent 支持在任务完成后自动移动文件到其它位置吗?</A><BR><A 
href="#What_is_AppData.3F">2.22 什么是 %AppData%?</A><BR><A 
href="#What_do_all_these_BitTorrent_terms_mean.2C_such_as_seeder.2C_snubbed.2C_etc.3F">2.23 
我想知道 BitTorrent 的专有名词的详细意义, 如做种等等?</A><BR><A 
href="#What_do_all_those_flags_in_the_Flags_column_mean.3F">2.24 
标识栏中的标识字符是什么意思?</A><BR><A 
href="#What_do_the_red_icons_mean_on_the_torrent_status_icons.3F">2.25 
任务状态图标的红色图标是什么意思 (<IMG alt="Tracker 服务器错误 (下载)" 
src="images/reddown.png">/<IMG alt="Tracker 服务器错误 (上传)" 
src="images/redup.png">)?</A><BR><A 
href="#What_do_all_the_status_icons_mean.3F">2.26 
所有状态图标的详细含义是什么?</A><BR><A 
href="#What_are_labels_and_what_can_they_be_used_for.3F">2.27 
标签是什么? 标签有什么用?</A><BR><A 
href="#What_do_the_colors_in_the_Availability_graph_mean.3F">2.28 
健康度图表中各种颜色的详细含义是什么?</A><BR><A 
href="#What_do_all_those_colors_in_the_Pieces_tab_mean.3F">2.29 
分块标签页中各种颜色各有什么含义?</A><BR><A 
href="#What_do_the_colors_in_the_files_tab_mean.3F">2.30 
文件标签页中各种颜色各有什么含义?</A><BR><A 
href="#What_does_Wasted_and_hashfails_mean.3F">2.31 
丢弃错误数据和校验失败是什么意思?</A><BR><A 
href="#What_does_availability_mean.3F">2.32 健康度是什么意思?</A><BR><A 
href="#What_does_Force_Start_do.3F">2.33 强制开始是什么意思 (正在下载 [强制]/正在做种 [强制])?</A><BR><A 
href="#What_does_Bandwidth_Allocation_do.3F">2.34 
带宽分配功能有何作用?</A><BR><A 
href="#How_is_the_share_ratio_shown_for_torrents_that_are_started_fully_complete.3F">2.35 
任务的分享率是什么意思? 如何计算?</A><BR><A 
href="#What_is_the_Logger_tab_and_what_does_it_do.3F">2.36 
日志标签页有何作用?</A><BR><A 
href="#What_is_that_magnifying_glass_and_box_for.3F">2.37 
工具栏上的放大镜图标是什么? 有何作用?</A><BR></P></DIV>
<DIV class=tocline><A href="#Features">3 
功能</A><BR></DIV>
<DIV class=tocindent>
<P><A 
href="#Does_.C2B5Torrent_support_Protocol_Encryption.3F">3.1 
µTorrent 支持协议加密以对抗 ISP 封锁 P2P 软件吗?</A><BR><A 
href="#Does_.C2.B5Torrent_support_DHT_or_Peer_Exchange.3F">3.2 
µTorrent 支持 DHT 网络或用户来源交换吗?</A><BR><A 
href="#Does_.C2.B5Torrent_have_a_plugin_system.3F">3.3 
µTorrent 拥有类似 Winamp 的插件系统吗?</A><BR><A 
href="#Does_.C2.B5Torrent_support_UNC-style_paths_.28e.g._.2F.2F192.168.1.2.2FC.24.2F_.29_.2F_network_drives.3F">3.4 
µTorrent 支持网络路径 (如 \\192.168.1.2\C$\ ) 或网络驱动器吗?</A><BR><A 
href="#Does_.C2.B5Torrent_have_Unicode_support.3F">3.5 
µTorrent 支持 Unicode 吗?</A><BR><A 
href="#Does_.C2.B5Torrent_support_multi-tracker_torrents.3F">3.6 
µTorrent 支持多 Tracker 服务器的任务吗?</A><BR><A 
href="#Does_.C2.B5Torrent_support_UPnP_.28Universal_Plug.27n.27Play.29.3F">3.7 
µTorrent 支持 UPnP (通用即插即用) 吗?</A><BR><A 
href="#Does_.C2.B5Torrent_support_RSS_feeds.3F">3.8 
µTorrent 支持 RSS 吗?</A><BR><A 
href="#Does_.C2.B5Torrent_support_Super_Seeding_mode.3F">3.9 
µTorrent 支持超级种子模式吗?</A><BR><A 
href="#Does_.C2.B5Torrent_support_endgame_mode.3F">3.10 
µTorrent 支持 Endgame 模式吗(用于在下载接近完成时快速完成)?</A><BR><A 
href="#Does_.C2.B5Torrent_allow_selective_file_downloading.3F">3.11 
µTorrent 支持选择性的下载任务中的某些文件吗?</A><BR><A 
href="#Does_.C2.B5Torrent_support_Manual_Announce.3F">3.12 
µTorrent 支持手动连接服务器吗?</A><BR><A 
href="#Does_.C2.B5Torrent_support_HTTPS_.28SSL.29_or_UDP_trackers.3F">3.13 
µTorrent 支持 HTTPS (SSL) 或 UDP tracker 服务器吗?</A><BR><A 
href="#Does_.C2.B5Torrent_support_trackerless_torrents.3F">3.14 
µTorrent 支持无服务器/无种子下载吗?</A><BR><A 
href="#Does_.C2.B5Torrent_have_an_embedded_tracker.3F">3.15 
µTorrent 拥有内嵌 Tracker 服务器吗?</A><BR><A 
href="#Does_.C2.B5torrent_support_multi-scrape.3F">3.16 
µTorrent 支持多次查询种子进行状态吗?</A><BR><A 
href="#Does_.C2.B5Torrent_have_a_boss_key.3F">3.17 
µTorrent 支持老板键功能吗?</A><BR><A 
href="#Does_.C2.B5Torrent_automatically_ban_peers_after_a_certain_number_of_hashfails.3F">3.18 
µTorrent 支持在对获得的数据校验失败达到一定数量时自动阻止相应用户吗?</A><BR><A 
href="#Does._C2.B5Torrent_have_a_BitComet_style_add_torrent_dialog.3F">3.19 
µTorrent 拥有类似 BitComet 式样的添加任务对话框吗?</A><BR><A 
href="#Does.C2.B5Torrent_have_a_web_interface.3F">3.20 
µTorrent 拥有网络接口吗(支持远程控制)?</A><BR><A 
href="#Can_you_make_.C2.B5Torrent_automatically_run_a_program_after_the_download_finishes.3F">3.21 
如何使 µTorrent 在下载任务完成后自动运行某个程序?</A><BR><A 
href="#Is_there_any_foreign_language_support_for_.C2.B5Torrent.3F">3.22 
µTorrent 支持多国语言吗?</A><BR></P></DIV>
<DIV class=tocline><A href="#Installation">4 
安装</A><BR></DIV>
<DIV class=tocindent>
<P><A 
href="#Does_.C2.B5Torrent_install_itself.3F">4.1 
µTorrent 支持自动安装吗?</A><BR><A 
href="#Where_are_the_settings_and_.torrent_files_stored.3F">4.2 
参数设置和种子文件保存到哪个位置?</A><BR><A 
href="#How_do_I_uninstall_.C2.B5Torrent.3F">4.3 
如何卸载 µTorrent?</A><BR><A 
href="#How_can_I_use_.C2.B5Torrent_on_a_USB_key_or_other_removable_drive.3F">4.4 
如何在闪盘或其它可移动存储设备中使用 µTorrent?</A><BR><A 
href="#How_can_I_share_my_torrents_between_user_profiles.3F">4.5 
如何在多用户环境中使用 µTorrent?</A><BR><A 
href="#How_can_I_backup_my_settings.3F">4.6 如何备份我的参数设置?</A><BR><A 
href="#How_can_I_reset_the_settings_back_to_the_defaults.3F">4.7 
如何重置所有参数为默认值?</A><BR><A 
href="#How_can_I_change_the_system_tray_icon_.2F_GUI_icon_for_.C2.B5Torrent.3F">4.8 
如何更改系统托盘图标或程序界面图标?</A><BR><A 
href="#Can_I_change_.C2.B5Torrent.27s_skin.3F">4.9 
µTorrent 支持皮肤功能吗?</A><BR></P></DIV>
<DIV class=tocline><A href="#Network">5 
网络</A><BR></DIV>
<DIV class=tocindent>
<P><A 
href="#Does_.C2.B5Torrent_work_well_on_Windows_XP_SP2_systems_with_an_unpatched_TCPIP.SYS.3F">5.1 
µTorrent 可以在没有修改并发连接数的  Windows XP SP2 系统上正常工作吗?</A><BR><A 
href="#Help.21_.C2.B5Torrent_is_sending_e-mails.2Faccessing_the_web.2Fetc.21">5.2 
求助! 防火墙或反病毒程序报告 µTorrent 正在发送电子邮件或访问网络!</A><BR><A 
href="#My_firewall_is_reporting_connections_being_made_by_.C2.B5Torrent_on_a_port_besides_the_one_I_selected._What_gives.3F">5.3 
防火墙程序报告 µTorrent 使用的端口不是我所选择的端口. 这是什么原因?</A><BR>
<A href="#How_do_I_change_the_number_of_connections_.C2.B5Torrent_uses.3F">5.4 
如何更改 µTorrent 使用的连接数?</A><BR><A 
href="#What_do_the_Network_Status_lights_mean.3F">5.5 
状态栏的网络状态图标有何含义 (<IMG alt="灯为绿色" 
src="images/greennetwork.png">, <IMG alt="灯为黄色" 
src="images/yellownetwork.png">, <IMG alt="灯为红色" 
src="images/rednetwork.png">)?</A><BR><A 
href="#Why_are_my_torrents_going_so_slow.3F">5.6 
为什么我的下载速度始终很慢?</A><BR><A 
href="#How_do_I_forward_ports.3F">5.7 如何转发端口?</A><BR><A 
href="#What_ports_should_I_use_for_.C2.B5Torrent.3F">5.8 
µTorrent 工作时使用哪个端口?</A><BR><A 
href="#How_do_I_change_the_port_.C2.B5Torrent_uses.3F">5.9 
如何更改 µTorrent 使用的端口?</A><BR><A 
href="#Does_.C2.B5Torrent_support_proxies.3F">5.10 
µTorrent 支持代理服务器吗?</A><BR><A 
href="#Does_.C2.B5Torrent_have_a_scheduler_to_limit_download_and_upload_speeds.3F">5.11 
µTorrent 支持使用计划任务限制下载和上传速度吗?</A><BR><A 
href="#How_can_I_make_.C2.B5Torrent_use_a_different_upload_speed_when_seeding.3F">5.12 
如何设置 µTorrent 在做种时使用不同的上传速度?</A><BR><A 
href="#How_can_I_make_.C2.B5Torrent_report_a_different_IP_to_the_tracker.3F_I.27m_behind_a_proxy_and_need_this_function.">5.13 
如何使 µTorrent 在客户端的 IP 地址更改时向服务器报告 IP 地址? 我使用了代理服务器所以需要这一功能.</A><BR><A 
href="#How_can_I_make_.C2.B5Torrent_use_a_specific_network_adapter.3F">5.14 
如何使 µTorrent 使用指定的网卡工作?</A><BR><A 
href="#How_can_I_change_the_number_of_active_torrents.2Fdownloads.3F">5.15 
如何设置最多同时进行的任务数和下载任务数?</A><BR></P></DIV>
<DIV class=tocline><A href="#Error_Messages">6 
错误消息</A><BR></DIV>
<DIV class=tocindent>
<P><A 
href="#While_torrenting_I_get_error_the_system_cannot_find_the_path_specified">6.1 
下载或做种时 µTorrent 提示 "错误: 系统没有找到指定的路径" 然后挂起任务.</A><BR><A 
href="#How_do_I_fix_Error_unable_to_save_the_resume_file.3F">6.2 
如何解决 "错误: 无法保存续传信息文件"?</A><BR><A 
href="#What_does_Download_Limited_in_the_status_bar_mean.3F">6.3 
状态栏中的 "下载已被限制" 是什么意思?</A><BR><A 
href="#What_does_Disk_Overload_in_the_status_bar_mean.3F">6.4 
状态栏中的 "磁盘负荷过大" 是什么意思?</A><BR><A 
href="#What_does_Unable_to_map_UPnP_port_to_mean.3F">6.5 
"无法映射 UPnP 端口到 xx.xx.xx.xx:xx" 等类似提示是什么意思?</A><BR><A 
href="#What_does_Error_opening_Windows_Firewall_mean.3F">6.6 
"打开 Windows 防火墙出错, 代码: 0x800706D9" 是什么意思?</A><BR><A 
href="#I_get_Error_Not_enough_free_space_on_disk_when_I_have_enough_free_space">6.7 
µTorrent 提示 "错误: 磁盘剩余空间不足." 但是我确信磁盘空间足够!</A><BR><A 
href="#Error_Custom_001">6.8 
µTorrent 在与服务器通讯时假死且提示 "向一个无法连接的网络尝试了一个套接字操作." </A><BR><A 
href="#Error_Custom_002">6.9 
µTorrent 提示 "错误: 访问被拒绝" 或 "错误: 进程无法访问文件, 该文件正被另一进程使用" 然后自动挂起任务.</A><BR><A 
href="#Error_Custom_003">6.10 
µTorrent 提示 "无法执行套接字操作, 系统缓冲空间不足或队列已满" 然后失去响应.</A><BR><A 
href="#I_get_Error_Element_not_found_and_the_torrent_stops">6.11 
µTorrent 提示 "错误: 没有找到组件" 然后挂起任务.</A><BR><A 
href="#I_get_Error_Parameter_is_incorrect_when_selectively_downloading">6.12 
µTorrent 提示 "错误: 参数不正确" (运行于 Windows 95/98/ME 系统中并进行选择性下载时)</A><BR><A 
href="#I_get_Error_Data_Error_cyclic_redundancy_check">6.13 
我遇到提示消息 "错误: 数据错误 (循环冗余检查)" 然后 μTorrent 挂起任务.</A><BR></P></DIV>
<DIV class=tocline><A href="#Troubleshooting">7 
问题解决</A><BR></DIV>
<DIV class=tocindent>
<P><A 
href="#Why_is_my_torrent_stuck_at_a_certain_percent.3F">7.1 
为什么任务进度始终停止在某个百分比上不再上升?</A><BR><A 
href="#Why_doesn.27t_.C2.B5Torrent_report_me_as_a_seeder_when_selectively_downloading.3F">7.2 
为什么进行选择性下载并完成后 µTorrent 不将我报告为种子?</A><BR><A 
href="#Why_does_pause_mode_keep_downloading_or_uploading.3F">7.3 
为什么暂停模式下依然存在下载速度和上传速度?</A><BR><A 
href="#Why_does_it_show_download_speed_when_seeding.3F">7.4 
为什么在做种时仍然有 0.1-0.2 KiB/s 的下载速度?</A><BR><A 
href="#Why_is_there_an_ETA_when_seeding.3F">7.5 
为什么在做种时仍然显示剩余时间?</A><BR><A 
href="#Why_do_the_up.2Fdown_buttons_not_move_the_torrent.3F">7.6 
为什么上移/下移按钮并不移动任务?</A><BR><A 
href="#Why_does_.C2.B5Torrent_create_files_I_set_to_.22Don.27t_Download.3F.22">7.7 
为什么选择了不下载文件 µTorrent 却依然创建了相应文件?</A><BR><A 
href="#Why_can.27t_I_see_anything_in_the_directory_browser_dialog.3F">7.8 
为什么目录浏览对话框中什么内容也没有? </A><BR><A 
href="#Why_do_my_torrents_grind_to_a_halt_with_disk_overload_whenever_I_add_a_new_one.3F">7.9 
为什么添加的任务被挂起且 µTorrent 提示 "磁盘负荷过大"?</A><BR><A 
href="#Why_does_.C2.B5Torrent_use_FixedSys_on_Windows_NT_4.3F">7.10 
为什么在 Windows NT4 系统中 µTorrent 使用 FixedSys 字体?</A><BR><A 
href="#.C2.B5Torrent_won.27t_open_torrent_files_even_though_I_associated_torrents_with_it">7.11 
为什么设置了文件关联后 µTorrent 仍然无法打开种子文件?</A><BR></P></DIV>
<DIV class=tocline><A href="#Misc">8 
其它</A><BR></DIV>
<DIV class=tocindent>
<P><A 
href="#I_found_a_bug.2C_what_do_I_do.3F">8.1 我发现了程序的一个错误, 我应该怎么办?</A><BR><A 
href="#My_question_isn.27t_answered_here_.2F_I_wanna_request_a_feature.2C_is_there_somewhere_I_can_go.3F">8.2 
我没有在这里找到我碰到的问题的答案或是我希望作者开发新的功能, 我应该怎么办?</A><BR><A 
href="#Does_.C2.B5Torrent_work_on_a_486_with_Windows_95_and_14MiB_of_RAM.3F">8.3 
µTorrent 可以运行于 486 平台且只拥有 14 兆内存的 Windows 95 系统中吗?</A><BR><A 
href="#What_is_DHT.3F">8.4 什么是 DHT 网络?</A><BR><A 
href="#Why_does_.C2.B5Torrent_show_less_DHT_nodes_in_the_status_bar_than_BitComet_Azureus.3F">8.5 
为什么 µTorrent 状态栏上显示的已连接 DHT 节点数比 BitComet 或 Azureus 少?</A><BR><A 
href="#Does_DHT_mean_my_torrents_from_private_trackers_are_getting_leaked.3F">8.6 
使用 DHT 会使来自私有 Tracker 服务器的种子泄露吗?</A><BR><A 
href="#I_don.27t_want_DHT_on_anyway.2C_how_do_I_turn_it_off.3F">8.7 
我不想使用 DHT 网络, 应该如何关闭?</A><BR><A 
href="#Can_you_implement_password_locking.3F">8.8 
µTorrent 支持密码锁定功能吗?</A><BR><A 
href="#Can_you_implement_manual_client_banning.3F">8.9 
µTorrent 支持手动阻止客户端吗?</A><BR><A 
href="#What_do_all_those_settings_in_Advanced_do.3F">8.10 
高级选项中的各个参数的含义是什么?</A><BR><A 
href="#What_is_ipfilter.dat.3F">8.11 文件 ipfilter.dat 有什么作用?</A><BR><A 
href="#What_is_flags.conf.3F">8.12 文件 flags.conf 有什么作用?</A><BR><A 
href="#Who_makes_.C2.B5Torrent.3F">8.13 µTorrent 的作者是谁?</A><BR><A 
href="#How_can_.C2.B5Torrent_be_so_small_and_so_fast.3F">8.14 为什么 µTorrent 体积如此之小且运行速度飞快?</A><BR><A 
href="#How_do_you_write_.C2.B5.3F">8.15 如何输入字符 µ?</A><BR><A 
href="#How.2Fwhere_can_I_get_the_latest_.C2.B5Torrent_beta.3F">8.16 从哪里可以获得最新版本的 µTorrent?</A><BR></P></DIV></DIV></DIV>
<DIV class=clearfix id=innercontent style="width: 720px"><BR>
<H2><A name=Incompatible_software></A>兼容性</H2>
<H3><A 
name=Help.21_My.C2.B5Torrent_process_is_using_a_ton_of_memory.21></A>为什么我的 µTorrent 进程占用了非常大的内存? (可能由 NVIDIA 防火墙引起)</H3>
<P>如果你发现 µTorrent 进程反常的使用了大量的内存或内存的使用量持续上升 (特别是在启用了 DHT 网络时), 并且你使用了 
NVIDIA 防火墙, <B>请卸载 NVIDIA 防火墙. 仅仅禁用防火墙是无效的!</B> 这个防火墙会导致许多程序运行时会出现内存泄露的问题. 这个问题 
<B>不是</B> µTorrent 的问题. 目前尚未有此问题的解决办法, 你必须卸载 NVIDIA 防火墙. 新版本的 NVIDIA 防火墙 (捆绑于最新版本的 nForce 驱动) 似乎已经修复了这一问题. 但 µTorrent 仍然保留了这一警告信息, 这一信息将在以后的版本中去除. 
</P>
<P>信息来源: <br><A 
href="http://forum.emule-project.net/lofiversion/index.php/t86089.html">http://forum.emule-project.net/lofiversion/index.php/t86089.html</A> 
<BR><A 
href="http://forums.nvidia.com/lofiversion/index.php?t2682.html">http://forums.nvidia.com/lofiversion/index.php?t2682.html</A> 
</P>
<H3><A name=My.C2.B5Torrent_keeps_freezing_on_certain_trackers></A>为什么我的 µTorrent 在与服务器通讯时假死且提示 "向一个无法连接的网络尝试了一个套接字操作." (可能由 BitDefender 或其它程序引起)</H3>
<P><B>这可能是由 BitDefender 导致的, 不是 µTorrent 的问题.</B> 很遗憾的是, 禁用 BitDefender 是没有用的, <B>你必须卸载 BitDefender</B> 才能解决这一问题. 你可以使用其它反病毒软件来代替 BitDefender.</P>
<P>如果你确信这一问题不是由 BitDefender 引起的, 请尝试卸载或禁用防火墙程序. 如果能够正常工作的话, 请到 
<A href="http://forum.utorrent.com/">µTorrent 论坛</A> 发帖说明你使用的防火墙的名称. <BR>如果你没有使用防火墙软件的话, 请确认你已经安装了网卡的最新驱动, 因为这一问题也有可能是由错误的网卡驱动造成的. 
其它原因还包括网络连接被断开或是调制解调器/路由器被关闭.</P>
<H3><A name=My_.C2.B5Torrent_window_doesnt_update_or_refresh></A>为什么我的 µTorrent 窗口不自动更新或刷新? (可能由 Stardock ObjectDock 引起)</H3>
<P>Stardock ObjectDock 有时会使 µTorrent 以及其它一些程序的窗口冻结并停止更新. 程序本身仍然在正常运行, 仅仅界面不再刷新而已. 你可以尝试卸载 
ObjectDock 并使用其它类似程序, 如免费的 YzDock 来代替 ObjectDock. </P>
<H3><A 
name=I_get_Error_Access_Denied_and_.C2.B5Torrent_halts_the_torrent></A>我在使用 µTorrent 时遇到 "错误: 访问被拒绝" 或 "错误: 进程无法访问文件, 该文件正被另一进程使用." 等类似的提示然后 µTorrent 自动挂起任务的情况. (Google/MSN Desktop, 某些反病毒软件, WinZIP 快速查找功能, 资源管理器开启缩略图及其它原因)</H3>
<P>"错误: 访问被拒绝" 可能是由诸多原因造成的. 最常见的原因可能是安装了 Google/MSN Desktop.  解决办法之一是关闭软件的索引功能或卸载此类软件. 另外添加下载文件夹到索引的排除列表中应该也能解决问题.
解决办法之二是禁用高级选项中的 diskio.flush_files 功能, (diskio.flush_files 的作用是使 μTorrent 每分钟关闭一次文件句柄, 这有助于解决 Windows 不完善的系统缓存管理所带来的问题), 而 Google/MSN Desktop 每分钟都尝试打开这些文件, 导致 μTorrent 无法写入, 从而出现错误提示. 最好的解决办法是卸载 Google/MSN Desktop.</P>
<P>同理, 建议禁用 WinZIP 提供的快速查找功能.</P>
<P>出现这一提示还可能是由于你重复运行 µTorrent 程序并重复添加了同一任务. 建议运行任务管理器并终止掉多余的 utorrent.exe 进程. 如果确定是这个原因导致 µTorrent 出错, 建议终止所有 utorrent.exe 进程后再重新运行程序.</P>
<P>这一提示也有可能是杀毒软件的实时监控或扫描功能引起的, 但是目前尚未发现有杀毒软件存在这一问题. 如果确定是这一原因导致的, 请尝试卸载杀毒软件或禁用其实时监控或扫描功能.</P>
<P>AVG7 可能也是使 μTorrent 出错的元凶, 一般在 AVG 7 处理某些存在 JPEG 溢出攻击代码的文件时出现这一错误提示.</P>
<P>其他可能性还包括查看下载保存文件夹时使用了缩略图模式或是资源管理器尝试读取媒体文件. 你可以反注册媒体预览功能 (开始菜单 -&gt; 
运行 -&gt; 输入 regsvr32 /u shmedia.dll ), 或是反注册图像预览功能 (开始菜单 
-&gt; 运行 -&gt; 输入 regsvr32 /u shimgvw.dll ).</P>
<P>出现这一提示还可能是由于文件的属性为只读, 这样 µTorrent 将无法写入该文件从而导致出错. 如果是在尝试做种时出现这一提示, 则说明原文件已损坏或校验失败. 
check.</P>
<H3><A 
name=My.C2.B5Torrent_freezes_locks_up_sometimes_and_or_uses_100_CPU></A>为什么我的 
µTorrent 有时假死或 CPU 使用达到 100% 从而减慢了我的计算机的运行速度? </H3>
<P>这可能是由 Avast! 的 P2P shield 扫描 P2P 程序导致的. 禁用该选项可解决这一问题.<BR>下面是操作流程 (感谢 scarface_666 提供):</P>
<UL>
  <LI>鼠标左键双击系统托盘中的 Avast! 图标. 
  <LI>鼠标左键点击底部的 "Details(详细信息) &gt;&gt;" 按钮. 
  <LI>找到位于左侧的 P2P Shield 功能并点击它使之高亮. 
  <LI>然后点击 "Terminate(终止)" 按钮然后你会看到提示文字 "No task is currently using this provider(当前没有任务使用这一提供商)". 
  <LI>最后点击 "确定" 按钮. </LI></UL>
<P>你还可以尝试在 P2P shield 的自定义排除程序中添加 utorrent.exe 以解决问题. </P>
<P>Spyware Doctor 5 会使 µTorrent 在安装时假死. 请卸载它或使用 v4 版本.</P>
<H3><A name=Kerio_Sunbelt_uses_100_CPU_when_running_.C2.B5Torrent></A>为什么 Kerio 或 Sunbelt 的个人防火墙在运行的时候 CPU 占用达到 100% 或断开了网络连接?</H3>
<P>Kerio 个人防火墙或 Sunbelt 个人防火墙程序存在一个错误导致在使用 µTorrent 会出现上述问题.</P>
<P>解决方如下:</P>
<UL>
  <LI>打开 Kerio 主界面 (建议此时退出 µTorrent) 
  <LI>点击 Overview(概览) 标签页 (左上角) 
  <LI>切换到 '连接' 页面 
  <LI>右键点击窗口中正使用的网络连接 
  <LI>取消选中 'Resolve Address(解析地址)' (从下往上第三个按钮) </LI></UL>
这会解决 KPF 100% CPU 使用的问题.<BR>出错原因是因为它尝试解析所有 µTorrent 连接的 IP 地址的主机名从而导致 CPU 使用达到 100%. 
<BR>同理可解决 Sunbelt 个人防火墙程序的问题.
<P>感谢 Noodlewad 提供解决方案.</P>
<H3><A name=Special_note_for_users_with_Linksys_WRT54G_GL_GS_routers></A>使用 Linksys WRT54G/GL/GS 路由器的用户请注意, 路由器在运行 P2P 程序时出现问题的解决方案</H3>
<P>下列步骤不适用与 WRT54G/GS v5 以上版本的路由器! 那些路由器请升级到最新版本的固件即可解决出现的各种问题 (1.00.9+).</P>
<P>Linksys 的默认固件(以及其它部分固件, 除了最新的 DD-WRT 和 HyperWRT Thibor 固件) 存在一个服务问题, 当它们跟踪最近五天的旧连接时, 
会导致在使用 P2P 程序时路由器失去响应. 启用 DHT 网络连接仅是加大这一情况出现机率的原因之一.</P>
<P>Linksys 已经确定了这一错误, 但是尚未给出解决方案. 如果你使用的是下列固件, 请将下列脚本添加到启动脚本中以解决这一问题. 
DD-WRT 和 HyperWRT 支持自定义启动脚本. </P>
<UL>
  <LI><A href="http://www.thibor.co.uk/">HyperWRT Thibor (14+) (仅 54G/GL/GS, 不包括硬件版本为 V5 的 54G/GS)</A> 
  <LI><A href="http://www.dd-wrt.org/">DD-WRT (v23+)(54G/GL/GS 不包括硬件版本为 V5 的 54G)</A> </LI></UL>
<P><B>请谨慎操作并做好备份, 责任自负. <A 
href="http://www.linksysinfo.org/forums/showthread.php?t=47259">点击查看路由器恢复方案</A></B></P>
<P>如果遇到奇怪的性能问题请清空路由器的 NVRAM, 如果还是不行请刷其它固件(但要注意刷新后要清空 NVRAM)</P>
<P><B>请不要使用无线连接刷新固件!</B></P>
<P>请严格按照 HyperWRT Thibor 下载页面上提供的刷新流程操作, 刷新前确认下载的固件版本是否正确!</P>
<P>DD-WRT v23+ 和 DD-WRT Thibor14+ 不需要这些步骤, 但是旧版本的 HyperWRT tofu/Thibor 需要. 如果你使用的是旧版本的 DD-WRT, 
则必须升级到 v23 SP2 及其后续版本. </P>
<UL>
  <LI><B>下列流程针对 HyperWRT tofu 及旧版本的 Thibors.</B> 
  <LI>在浏览器中输入 http://192.168.1.1 进入路由器的 Web 管理界面 (默认密码为 admin). 
  <LI>点击 "管理", 然后编辑启动. 输入下列命令:<BR></LI></UL><PRE>echo 1 &gt; /proc/sys/net/ipv4/icmp_echo_ignore_broadcasts
echo 1 &gt; /proc/sys/net/ipv4/icmp_ignore_bogus_error_responses
echo "600 1800 120 60 120 120 10 60 30 120" &gt; /proc/sys/net/ipv4/ip_conntrack_tcp_timeouts
</PRE>
<UL>
  <LI>点击 "保存", 关闭窗口, 然后点击保存设置. 
  <LI>重启路由器 (管理页面中有一个重启按钮). </LI></UL>
<P>要升级到 DD-WRT, 使用下列流程. <A 
href="http://www.dd-wrt.com/wiki/index.php/WRT54G_v4_Installation_Tutorial">WRT54G 
v4 安装教程</A>.</P>
<UL>
  <LI><B>下列流程仅针对 DD-WRT v23 及其后续版本.</B> 
  <LI>输入下列值于 'Web-Admin -&gt; Administration -&gt; 
  Management -&gt; IP Filter Settings' 
  <LI>Maximum Ports: 4096 
  <LI>TCP Timeout (s): 300 (如果 TCP 连接较多请减小此值) 
  <LI>UDP Timeout (s): 300 (如果 UDP 连接较多请减小此值) 
  <LI>保存设置并重启路由器 </LI></UL>
<P>如果想知道参数的具体含义请点击 <A 
href="http://www.dd-wrt.com/wiki/index.php/Router_Slowdown">http://www.dd-wrt.com/wiki/index.php/Router_Slowdown</A></P>
<P>下列页面拥有修复刷坏路由器的解决方案 <A 
href="http://www.linksysinfo.org/forums/showthread.php?t=47259">点击这里</A></P>
<P>请到 <A 
href="http://hyperwrt.org/">HyperWRT.org</A> (主站没有最新版本的固件), <A 
href="http://www.hyperwrt.org/forum/viewforum.php?id=15">HyperWRT 论坛高级帮助</A>, <A href="http://www.dd-wrt.org/">DD-WRT.org</A>, 以及 
<A href="http://wrt-wiki.bsr-clan.de/index.php?title=Main_Page">DD-WRT 维基百科</A>. <BR>
</P>
<H3><A 
name=Norton_keeps_prompting_whether_to_permit_or_block_.C2.B5Torrent.21></A>为什么 Norton 反病毒程序总是让我选择是允许还是阻止 µTorrent?</H3>
<P>这是由 NAV 的蠕虫病毒保护功能导致的, 解决方法如下:</P>
<UL>
  <LI>运行 Norton 反病毒程序 
  <LI>在左侧面板中选择 "Internet 蠕虫病毒保护". 
  <LI>在右侧面板中, 点击 "程序控制". 
  <LI>找到 µTorrent 项目并删除它. 
  <LI>点击 "添加" 按钮. 
  <LI>定位到 uTorrent.exe 
  <LI>在下一对话框中, 从下拉菜单中点击允许. 
  <LI>点击 "确定" 按钮. 
  <LI>如果还有询问有关 µTorrent 的窗口出现, 请点击 "允许". 
  <LI>搞定! 
</LI></UL>感谢 WIZZwatcher 提供此解决方案 
<P>另一解决方案是直接禁用 Internet 蠕虫保护功能, 因为这个功能并没有太大用处, 或是卸载 Norton 选用其它反病毒产品.</P>
<P>最近的 Norton Internet Security 2006 和 Norton AntiVirus 2006 应该解决了这一问题. </P>
<H3><A name=I_get_a_socket_buffer_space_error_and_the_client_halts></A>我遇到下列提示 "无法执行套接字操作, 系统缓冲空间不足或队列已满" 然后 µTorrent 失去响应.</H3>
<P>Norton GoBack 主要是其附带的 GBTray.exe 程序会导致出现这一问题. 你可以在运行 µTorrent 之前关闭 GBTray.exe 
即可, 如果仍然出现问题请禁用 Norton GoBack. 更新到最新的 v4.1 版本的 Norton Goback 可以完全解决这一问题.<BR>
这一问题也有可能是由 mIRC 崩溃并退出而导致的. 当它崩溃并退出时会使 CPU 使用达到 99%. 建议使用任务管理器手动终止其进程.<BR>
Windows 2000/XP/2003 的一项注册表设置也可能会导致出现这一问题, 请读取微软的下列文章并使用其提供的解决方案. <A 
href="http://support.microsoft.com/kb/Q196271">KB196271</A></P>
<P>这一问题还可能出现在没有破解 TCPIP.SYS 但将 µTorrent 中 net.max.halfopen 参数的值设置为超过 8 的情况下. 请使用 <A 
href="http://www.lvllord.de/?lang=en&amp;url=downloads">EventID 4226 
补丁"</A> (XP SP2 和 2003). 即使已经破解过该文件, 也应该经常检查以免被微软新的更新补丁覆盖而导致破解失效.</P>
<H3><A 
name=Modems_routers_that_are_known_to_have_problems_with_P2P></A>目前已知的与 P2P 程序存在兼容性的调制解调器或路由器名单
及解决办法 (来自 Azureus, 适用于 µTorrent)</H3>
<P>下面的列表中列出了目前所有已知的与 Azureus 以及 µTorrent 存在兼容性的硬件名单以及<b>解决办法</b>, 如果你使用的是下列硬件, 
建议先升级硬件固件. 如果你的路由器或调制解调器出现重启或假死现象, 
建议关闭 IP 解析, DHT 网络以及减少连接数再试. </P>
原因目前尚未查明: -_-
<UL>
  <LI>Apple Airport Extreme (802.11g 无线并使用 128 位 WEP 加密时)</LI></UL>
<H4>出错原因:&nbsp; 连接数设置过大</H4>
<UL>
  <LI>SpeedStream 5660 于路由器/NAPT 配置中. 目前尚未有高于 2.(3).7 版本的新版固件发布. 切换到 Bridge 
	防火墙模式可以修复这一问题. </LI></UL>
<P>下列路由器出现问题是因为全局连接数设置过大, 请将全局连接数的数值设置为低于 200 或更低:</P>
<UL>
  <LI>D-Link 302G 
  <LI>D-Link DI-624 
  <LI>D-Link DSL-G664T 
  <LI>Linksys BEFSR41V4/BESR41 
  <LI>Linksys Wireless-B 
  <LI>Netgear DG632 
  <LI>Netgear DG834G 
  <LI>Netgear MR814 
  <LI>Netgear WGT524 
  <LI>Netgear Rangemax 802.11n WPN824 
  <LI>W-Linx MB401-S (以及 SMC Barricade 7004 BR) 
  <LI>Westell 6100 </LI></UL>
<H4>出错原因: UPnP</H4>
<UL>
  <LI>Most D-Link 5xx and 6xx (连接数过大也会出错) 
  <LI>D-Link DI-604 
  <LI>Dynalink RTA1025W 
  <LI>TP-LINK TL-R410 
  <LI>ZyXEL Prestige 660H(W) (固件升级到 PE8+ 以上版本即可修复问题) 
  <LI>Some SpeedStreams </LI></UL>
<H4>出错原因: 端口转发</H4>
<UL>
  <LI>D-Link DI-514 (没有正确转发 UDP 协议) </LI></UL>
<H4>路由器出现假死或重启的解决办法</H4>
<UL>
  <LI>更新路由器的固件到最新版本 
  <LI>同时关闭客户端和路由器的 UPnP 功能而仅仅手动转发所需端口 
  <LI>将全局最大连接数设置为低于 200 或更低的数值 (可能会需要低于 100, 取决于路由器) 
  <LI>关闭 DHT 网络连接功能 
  <LI>调低 net.max_halfopen 数值大小以及 bt.connect_speed 数值大小<LI>将路由器置于 bridge(桥接) 或 gateway(网关) 
	模式 (不适用于那些无路由功能的调制解调器) 
  <LI>重新购买新的路由器或调制解调器 </LI></UL>
<P>来源: <A 
href="http://azureus.aelitis.com/wiki/index.php/Bad_routers">不兼容的路由器 - 
Azureus 维基百科</A> </P>
<H3><A 
name=I_get_tons_of_hashfails_on_my_torrents_and_the_torrent_never_finishes></A>我在进行下载任务时发现出现大量校验失败的情况, 并且该任务始终无法完成: 它始终停在 99.9%!</H3>
<P>如果在进行下载任务时出现大量校验失败的情况并且你使用的 D-Link 的路由器, 请关闭 DMZ 模式. 
<br>
DMZ 模式在 D-Link 路由器中称为游戏模式会破坏数据包导致校验失败从而无法完成下载. </P>
<H3><A name=I_get_very_high_CPU_use_when_running_.C2.B5Torrent.21></A>为什么 µTorrent 运行时 CPU 使用达到 100%? (可能与 McAfee 防火墙, SpamPal, Norman 个人防火墙或其它原因有关)</H3>
<P>这可能是由 McAfee 防火墙和 Norman 个人防火墙导致的. 请卸载它们并使用其它类似功能的产品代替. <BR>
还可能是由 SpamPal 的一个错误导致的. 请卸载它或使用 SpamPal v1.594. <BR>
Cybersitter 也会导致这一问题的出现, 卸载它即可.<BR><BR>
如果你没有安装上述软件而仍然会在使用 µTorrent 时出现 CPU 使用达到 100% 的情况, 请下载并运行 <A 
href="http://www.sysinternals.com/utilities/processexplorer.html">Sysinternals 
Process Explorer</A> 然后查看占用 CPU 使用率的 DPC. 
这可能是由于其它包含错误的软件, 包含错误的驱动或有缺陷的硬件导致的. 你可以运行微软提供的 <A 
href="http://www.microsoft.com/whdc/DevTools/tools/RATT.mspx">RATTV3</A> 来查找 DPC 的原因. 它的日志文件保存在 
C:\WINDOWS\system32\LogFiles\RATTV3 中.
<P>你还可以使用 Process Explorer 来检查 µTorrent 进程的属性, 点击 "线程" 标签页, 然后查看哪个 DLL被 hook 到进程中以查明原因. 
查看 mswsock.dll 是否正常. </P>
<H3><A name=.C2.B5Torrent_crashes.21></A>µTorrent 崩溃了... (可能与 V-Com 工具软件, Norman 个人防火墙及 McAfee 防火墙有关)</H3>
<P>V-Com 工具软件会导致 µTorrent 以及其它一些程序崩溃. 请卸载 V-Com 相关软件. <br>
Norman 个人防火墙和 McAfee 防火墙也会导致 µTorrent 崩溃, 如果 µTorrent 经常崩溃请尝试卸载它们.<br>
Cybersitter 也会导致 µTorrent 出现崩溃, 卸载它即可.</P>
<H3><A name=I_get_blank_dialogs_in_various_places></A>我在使用 µTorrent 
时遇到一些空白对话框或某些位置缺少文字. </H3>
<P>Norton 反病毒程序和 McAfee 反病毒程序拥有一项名为 "缓冲区溢出保护" 的功能. 请在反病毒软件的设置对话框中禁用该功能即可. </P>
<H2><A name=General></A>常规</H2>
<H3><A name=What_are_.C2.B5Torrent.27s_system_requirements.3F></A>µTorrent 的系统需求是什么</H3>
<P>非常低. 它最低可以工作于只有 14MB 内存运行 Windows 95 操作系统的 486 机器中 (当然, 需要安装 <A 
href="http://www.microsoft.com/windows95/downloads/contents/WUAdminTools/S_WUNetworkingTools/W95Sockets2/Default.asp">Winsock2</A> 
更新库), 最高支持到 2003 和 Vista 系统. 同样可运行于 64 位 Windows 系统中. </P>
<P>据网友测试表明, 在使用 Cedega 环境的 Linux 中以 "Win98 模式" 运行程序时可能在界面显示时会有一些小问题, 如颜色显示不正确,更新失败等问题.<BR>
µTorrent 可以非常好的运行于使用 Wine v0.9.16 以上环境的 Linux 系统中(特别是 µTorrent v1.6).<BR>
同样可以很好的运行于使用 Wine v0.9.16 以上环境的 FreeBSD v6.1 系统中, 但是需要系统安装并在 xorg.conf 中启用 GLX 模块 .<BR>
无法运行于 Darwine v0.9.12 或 OSX 中. 同样可运行于 CrossOver Office alpha 2 和 3 中, 但是最小化功能可能有一些问题. </P>
<P>所有 Win9x 系统最少需要安装 IE 4.0. 否则程序的界面及功能会不甚完整.</P>
<P>注意: Windows 95/98 系统限制了半开连接数最大为 100, 所以你需要先修改注册表项方可正常使用. (来源于 <A 
href="http://support.microsoft.com/kb/158474/EN-US/">微软官方说明</A>).<BR>
打开注册表编辑器 (开始 -&gt; 运行 -&gt; regedit), 转到分支 
HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\VxD\MSTCP<BR><BR>你必须创建下列键值, 默认情况下是不存在这个键值的.<BR>
键名: MaxConnections<BR>数据类型: 字符串<BR>指定最大半开连接数. 默认为 100. 最大可设置为 512.</P>
<H3><A name=Is_.C2.B5Torrent_open_source.3F></A>µTorrent 是开源软件吗?</H3>
<P>不是. 也不打算成为开源软件. </P>
<H3><A name=Is_there_a_Linux_or_Mac_version.3F></A>µTorrent 有 Linux 或 Mac 版本吗?</H3>
<P>当前还没有这两个平台的版本发布. 当时将来我们会考虑发布这两个平台的版本的 µTorrent. </P>
<P><b>但是你可以在 Wine 和 Cedega 环境中运行 µTorrent.</b> 
点击这里查看 <A href="#What_are_.C2.B5Torrent.27s_system_requirements.3F">系统需求</A>. </P>
<H3><A 
name=How_do_I_make_.C2.B5Torrent_prioritize_the_first_and_last_piece.3F></A>如何使 µTorrent 自动调整第一块和最后一块的优先级?</H3>
<P>打开参数设置对话框并切换到 "高级选项" 页面然后设置 "bt.prio_first_last_piece" 的值为 "启用" 即可. </P>
<H3><A 
name=How_do_I_make_.C2.B5Torrent_allocate_all_the_files_when_I_start_the_torrent.3F></A>如何使 µTorrent 在开始任务前为文件自动分配空间?</H3>
<P>选中参数设置对话框的 "下载" 页面中的 "下载前预先分配磁盘空间" 选项. 打开这个选项会避免压缩存储和稀疏文件的产生. 预先分配磁盘空间时, 只为要下载的文件分配空间, 选择了跳过的文件不会分配.
预先分配磁盘空间的作用仅仅是在开始任务前就确保有足够的磁盘空间. 与不预先分配磁盘空间相比, 并不会降低磁盘的碎片率, 因为无论预先分配磁盘空间是否开启 (除非你使用稀疏文件), µTorrent 都会在写入时自动调整写入位置以降低碎片率.  </P>
<H3><A name=How_do_I_modify_the_disk_cache_options.3F></A>如何修改磁盘缓冲参数?</H3>
<P>(1.6 及其后续版本) 默认设置即使在 100Mbps 的网络带宽下也可以很好的工作, 但是如果一定要 
设置磁盘缓冲参数的话, 可以打开参数设置对话框并双击 "高级选项", 然后切换到 "磁盘缓冲" 页面. 
要查看当前磁盘缓冲使用情况, 可以打开主界面上的 "图表" 标签页, 然后选择下拉框中的 "磁盘统计数据". </P>
<UL>
  <LI>缓冲大小是一个整体的大小, 无法为读取缓冲和写入缓冲分别指定大小. 
  <LI>&quot;磁盘缓冲有剩余时自动减少内存占用&quot; 可以在你不在进行下载时释放出写入缓冲. 
  <LI>&quot;每 2 分钟写入一次未使用的区块&quot; 可以控制 µTorrent 及时的将 2 分钟后仍未活动的数据块写入磁盘. 如果你的磁盘缓冲设置的很大或是在高速下载时建议不要选中此项. 
  <LI>&quot;实时写入下载完成区块的内容&quot; 可以控制 µTorrent 是否将完成的区块数据立即写入磁盘. 
  如果不选中此项, 则程序将在 15 秒后再写入磁盘. 如果你的磁盘缓冲设置的很大或是在高速下载时建议不要选中此项. 
  <LI>&quot;上传速度较慢则关闭读取缓冲&quot; 可以在上传速度低于 100KB/S 时关闭读取缓冲, 因为此时读取缓冲没什么太大用处. 
  <LI>&quot;从缓冲中删除旧区块内容&quot; 会在一定时间后从缓冲中删除没有使用的区块内容. 如果你的磁盘缓冲设置的很大建议不要选中此项. 
  <LI>&quot;缓冲不足时自动加大缓冲&quot; 会使 µTorrent 在磁盘负荷过大时自动增加磁盘缓冲大小. 
  </LI></UL>
<H3><A name=How_do_I_change_the_default_remove_action.3F></A>如何更改默认删除操作?</H3>
<P>(1.5.1 beta 460 及后续版本) 右键点击工具栏删除按钮, 然后按住 Shift 键并选择需要的默认选项. </P>
<H3><A 
name=How_do_I_make_.C2.B5Torrent_delete_files_to_the_recycle_bin.3F></A>如何使 µTorrent 删除文件到回收站?</H3>
<P>(1.5.1 beta 460 及后续版本) 右键点击工具栏删除按钮, 然后选中 "移动到回收站" 即可.</P>
<H3><A 
name=How_can_I_make_.C2.B5Torrent_auto-load_torrents_from_a_specified_folder.3F></A>如何使 µTorrent 从指定的文件夹中自动载入种子?</H3>
<P>只需在参数设置对话框中的 "其它" 页面中选中 "自动载入下列文件夹中的种子文件:" 并指定文件夹然后点击确定按钮. 当种子文件添加到该文件夹时, µTorrent 就会自动载入种子, 当然还会询问下载文件保存在哪里. 
<br>
如果你想自动保存到下载内容到指定文件夹, 切换到参数设置对话框中的 "下载" 页面, 启用 "下载文件保存到下列文件夹中:" 并指定要保存的文件夹. 这样就不会再出现 BitComet 式样的添加任务对话框了, 除非点击 文件 -> 打开种子文件 (非默认保存位置) 或启用 "始终显示添加任务对话框". 你可以选中 "载入时以删除种子文件代替重命名种子文件" 从而在载入后删除来源种子文件以代替重命名种子文件为 .torrent.loaded.

注意: 自动载入种子文件的文件夹不能是 %Appdata%\uTorrent, 也不能是你指定为 "种子文件保存在下列文件夹中:" 的那个文件夹. 如果两者指定了相同的文件夹就会出现错误!</P>
<H3><A name=How_can_I_rename_a_torrent_folder.3F></A>如何重命名任务的保存文件夹?</H3>
<P>如果你添加的是新种子, 可以在 BitComet 式样的添加任务对话框中改变任务的保存文件夹 (此文件夹可以不存在) 来重命名. 如果种子已经添加过了, 请先停止任务, 然后重命名你想保存任务的文件夹(如果已经下载了部分数据, 请将源目录中的数据复制到目标文件夹中), 然后在 µTorrent 中右键点击该任务, 选择 "高级" -> "设置保存位置", 指定到刚才重命名过的文件夹. 然后重新开始任务 (无需强制再检查).  </P>
<H3><A name=How_can_I_rename_a_torrent_in_the_main_listview.3F></A>如何重命名任务列表中的任务名称?</H3>
<P>(1.5.1 beta 460 及后续版本) 选中任务并按 F2 或单击任务名称. 按 Esc 键可取消重命名. </P>
<H3><A 
name=Is_it_possible_to_make_uTorrent_load_a_torrent_from_the_commandline.3F></A>µTorrent 支持从命令行载入种子文件吗?</H3>
<P>支持. 命令行格式如下: uTorrent.exe /directory "C:\Save Path" "D:\Some folder\your.torrent"<br>
其中保存路径的最后不要使用反斜杠, 否则命令会失败. 这种方法对单文件和多文件任务都适用. </P>
<H3><A 
name=How_can_I_make_.C2.B5Torrent_append_a_ut_extension_to_incomplete_files.3F></A>如何使 µTorrent 附加扩展名 !ut 到未完成下载的文件名之后以便区别?</H3>
<P>这个功能在 1.3.1 betas 版本中已加入. 打开参数设置对话框, 切换到 "下载" 页面并选中 "尚未完成下载的文件添加 !ut 后缀". 除了已开始, 停止, 暂停或队列的任务, 程序会立即在所有未完成下载的文件名称后添加 !ut 后缀. 关闭该功能就会撤消附加后缀名操作.  
</P>
<H3><A name=How_can_I_make_.C2.B5Torrent_start_minimized.3F></A>如何使 µTorrent 以最小化方式启动?</H3>
<P>只需从命令行启动时加入 /MINIMIZED 即可. 可以通过改变快捷方式 "目标" 属性即可随 Windows 系统启动时以最小化方式启动, 打开注册表编辑器 (开始菜单 -> 运行 -> regedit), 找到 HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run, 双击 µTorrent 项, 在命令行最后加上 /MINIMIZED. </P>
<H3><A name=How_can_I_make_.C2.B5Torrent_start_in_bosskey_mode.3F></A>如何使 µTorrent 以隐藏模式启动?</H3>
<P>(1.5.1 beta 460 及后续版本) 添加参数 /HIDE 到快捷方式的 "目标" 中. 请检查你是否已经设置好了老板键, 否则需要使用任务管理器结束进程而无法使用老板键呼出程序. </P>
<H3><A 
name=How_can_I_make_.C2.B5Torrent_go_into_a_seed-only_mode_on_all_torrents.3F></A>如何使 µTorrent 对所有任务进入仅做种模式?</H3>
<P>如果你想使程序按照计划任务在指定时间内自动进入仅做种模式, 可以在选择时间时按住 Shift (在 1.4.1 build 417 及后续版本中), 所选时间格会变红, 表示这是仅做种模式. 
如果你想手动设置某个下载任务进入仅做种模式, 你可以跳过任务中的所有文件, 在一分钟左右之后, 就会开始做种了. 设置后不会立即生效是因为要完成下载最后几个排队的数据块.</P>
<H3><A name=How_can_I_add_other_columns_of_info.3F></A>如何添加其他信息栏? 可否按多个标准对其进行排序操作?</H3>
<P>右键点击信息栏 (任一信息栏均可, 如名称或用户), 选择要添加或删除的信息栏. 
按住各个信息栏并拖动鼠标可实现对信息栏显示先后的排序. 单击信息栏可实现按该信息栏内容为标准的排序.</P>
<H3><A name=How_can_I_quickly_change_the_upload_and_download_caps.3F></A>如何快速更改上传和下载速度限制?</H3>
<P>右击状态栏上的 "上传:" 或 "下载:" 或右键点击系统托盘图标就可以更改. 另外也可以在参数设置的网络页面中设置.
你也可以找到高级选项 -> 用户界面, 选择 "启用状态栏右键限制速度菜单".  </P>
<H3><A 
name=How_can_I_tell_if_a_peer_is_an_incoming_or_outgoing_connection.3F></A>如何判断一个用户是连入连接还是连出连接?</H3>
<P>在用户标签页中, 观察 "标识" 栏. 如果该栏中显示有字母 I, 说明是连入连接. </P>
<H3><A 
name=How_can_I_make_.C2.B5Torrent_stop_seeding_at_a_specific_share_ratio.3F></A>如何使 µTorrent 在达到指定的分享率后停止做种?</H3>
<P>在参数设置的队列页面中, 设定你想达到的目标分享率/做种时间, 选中 "限制上传速度为" 并在后面的框中填入 0 即可. 
如果你还想继续上传并分配少量带宽, 则可以填入一个非零值. 
如果你想使做种任务比下载任务有更高的优先级, 选中 "做种任务的优先级高于下载任务的优先级", 这样下载任务会排队等待. 0 代表分享率为 0% , -1 为不限制分享率, 意为默认将所有任务始终设为做种, 你可以对每个任务手动设置分享率以决定哪些文件停止做种. 这样, 你仍需选中 "限制上传速度为" 并在后面的框中填入 0. </P>
<H3><A 
name=Can_.C2.B5Torrent_automatically_move_files_when_the_torrent_finishes.3F></A>µTorrent 支持在任务完成后自动移动文件到其它位置吗?</H3>
<P>支持, 点击菜单选项 -> 参数设置 -> 下载, 可以设置把完成后的任务移动到另外的文件夹或分区. 
如果没有为 "下载文件保存到下列文件夹:" 设置保存位置, 则必须取消选中 "仅从默认的保存文件夹中移动文件". <br>如果选中了 "附加任务标签到文件夹名称之后", 则拥有标签的任务在完成后会将该标签附加到任务的文件名称中. 
为了实现这一功能, 必须为 "下载完成后移动到下列文件夹中:" 设置保存目录.
<BR>查看 <A 
href="#What_are_labels_and_what_can_they_be_used_for.3F">标签是什么?标签有什么用?</A></P>
<H3><A name=What_is_AppData.3F></A>什么是 %AppData%?</H3>
<P>%AppData% 是 Windows 操作系统的一个系统变量.
要打开它, 你可以在地址栏中键入 %AppData%\uTorrent, 或点击 开始菜单 -> 运行, 再键入上述字符后按下 Enter 键.</P>
<H3><A 
name=What_do_all_these_BitTorrent_terms_mean.2C_such_as_seeder.2C_snubbed.2C_etc.3F></A>我想知道 BitTorrent 的专有名词的详细意义, 如做种等等?</H3>
<P>请查看 <A href="http://btfaq.com/serve/cache/23.html">BTFAQ.com's Definitions of 
BitTorrent Terms</A> </P>
<H3><A name=What_do_all_those_flags_in_the_Flags_column_mean.3F></A>标识栏中的标识字符是什么意思?</H3>
<UL>
  <LI>D = 当前正在下载 (感兴趣且没有被阻塞) 
  <LI>d = 本级客户端需要下载, 但是对方没有发送 (感兴趣但是被阻塞) 
  <LI>U = 当前正在上传 (感兴趣且没有被阻塞) 
  <LI>u = 对方需要本机客户端进行上传, 但是本机客户端没有发送 (感兴趣但是被阻塞) 
  <LI>O = 开放式未阻塞 
  <LI>S = 对方被置于等待状态 
  <LI>I = 对方为一个连入连接 
  <LI>K = 对方没有阻塞本机客户端, 但是本机客户端不感兴趣 
  <LI>? = 本机客户端没有阻塞对方, 但是对方不敢兴趣 
  <LI>X = 对方位于通过来源交换获取到的用户列表中 (PEX) 
  <LI>H = 对方已被通过 DHT 获取. 
  <LI>E = 对方使用了协议加密功能 (所有流量) 
  <LI>e = 对方使用了协议加密功能 (握手时) 
  <LI>L = 对方为本地/内网用户 (通过网络广播发现或位于同一网段中) </LI></UL>
<P>点击 <A href="http://btfaq.com/serve/cache/23.html">这里</A> 查看详细内容(英文) </P>
<H3><A name=What_do_the_red_icons_mean_on_the_torrent_status_icons.3F></A>任务状态图标的红色图标是什么意思 (<IMG alt="Tracker Error (download)" 
src="images/reddown.png">/<IMG alt="Tracker Error (upload)" 
src="images/redup.png">)?</H3>
<P>红色图标表明 µTorrent 无法连接到 Tracker 服务器. Tracker 服务器不在线, 过载或域名不再存在时可能会出现该图标. 请在常规标签页中查看服务器信息, 看看确切的出错信息是什么.
<br>很多时候看到这个图标(特别是在看到出错状态信息为找不到主机)并且打开了连接 DHT 网络功能但仍然没有连接用户时意味着你需要添加任务的 Tracker 服务器. 
<br>一般来说这都是因为 Tracker 服务器暂时过载或暂时离线, 它们很快就回恢复. 这对任务没什么太大影响, 任务本身会继续保持做种/下载状态, 你无需停止任务或退出程序.</P>
<H3><A name=What_do_all_the_status_icons_mean.3F></A>所有状态图标的详细含义是什么?</H3>
<P><IMG alt="" src="images/downloading.png"> 表示任务正在下载<BR><IMG alt="" src="images/reddown.png"> 表示任务正在上传但是 tracker 服务器出错(查看上个问题)<BR><IMG alt="" 
src="images/seeding.png"> 表示任务正在做种<BR><IMG alt="" 
src="images/redup.png"> 表示任务正在做种但是 tracker 服务器出错(查看上个问题)<BR><IMG alt="" src="images/queueddown.png"> 
表示任务处于下载队列中<BR><IMG alt="" src="images/queuedseed.png"> 表示任务处于上传队列中<BR><IMG alt="" src="images/stopped.png"> 
表示任务处于停止状态 <BR><IMG alt="" 
src="images/finished.png"> 表示任务处于停止的做种状态<BR><IMG alt="" 
src="images/paused.png"> 表示任务处于暂停状态<BR><IMG alt="" 
src="images/error.png"> 表示任务出错 (请检查状态列查看详细出错信息) </P>
<H3><A name=What_are_labels_and_what_can_they_be_used_for.3F></A>标签是什么? 标签有什么用?</H3>
<P>标签是 µTorrent 中一个强大的功能. 只需查看标签就可以识别任务的状态, 或者根据标签更方便地实现任务分类. <br>例如, 你可以把从私有服务器下载的任务贴上 "私有" 标签, 然后只需点击标签栏就可以对所有私有任务实现分类了. 你可以选择一个或一个以上的任务贴上标签. 
只需在任务上点击鼠标右键, 选择 标签 -&gt; "新建标签..." 就可以添加标签了, 当然也可以选择已经存在的标签. <br>
即使任务已有标签也可以更改标签. 要删除任务的标签, 选择 标签 -&gt; 删除标签 即可. 
不再使用的标签会自动从列表中删除.

如果你想设置永久标签, 可以在 高级选项 -> 用户界面 中设置.

正常和永久标签的作用还在于可以与参数设置的下载中的 "下载完成后移动到下列文件夹中" 选项联合使用. 标签会作为完成下载的任务的附加文件名称. 参见 <A 
href="#Can_.C2.B5Torrent_automatically_move_files_when_the_torrent_finishes.3F">µTorrent 支持在任务完成后自动移动文件到其它位置吗?</A></P>
<H3><A name=What_do_the_colors_in_the_Availability_graph_mean.3F></A>健康度图表中各种颜色的详细含义是什么?</H3>
<UL>
  <LI>红色表示你所连接的用户都不提供这一块数据的种子. (意味着健康度小于 1 且可能无法完成下载) 
<LI>灰蓝色表示你所连接的用户提供这一块数据的的种子, 但很少. 
<LI>深一点的蓝色, 群集中这一块数据的种子很多. </LI></UL>
<H3><A name=What_do_all_those_colors_in_the_Pieces_tab_mean.3F></A>分块标签页中各种颜色各有什么含义?</H3>
<UL>
  <LI>深蓝色表示数据已下载并已写入磁盘. 
  <LI>稍浅一点的蓝色表示数据已下载但仍在磁盘缓存中. 
  <LI>浅蓝色表示数据已向一个用户提交了下载请求. 
  <LI>绿色表示数据已向多个用户提交了下载请求 (仅出现于 <A 
  href="#Does_.C2.B5Torrent_support_endgame_mode.3F">Endgame</A> 模式).</LI></UL>
<H3><A name=What_do_the_colors_in_the_files_tab_mean.3F></A>文件标签页中各种颜色各有什么含义?</H3>
<P>蓝色表示数据已被写入磁盘. 绿色表示数据块已下载.</P>
<H3><A name=What_does_Wasted_and_hashfails_mean.3F></A>丢弃错误数据和校验失败是什么意思?</H3>
<P>丢弃错误数据显示的是丢弃的数据和错误的数据的大小. 丢弃数据是指丢弃了客户端没有连接的用户发来的数据. 校验失败是指接收到错误数据后哈希校验失败,并且数据块的完整性检查失败.
<b>无需为出现上述提示而担心</b>. 因为客户端会自动重新下载丢弃的数据和出错的数据并保证下载数据的正确性. 当然, 如果检验失败的次数反常(几百次/兆), 则这可能是一个有害的任务. 如果任务无法完成 (停止在 99.9% 且出现大量哈希检验失败), 则可能是路由器破坏了数据包.
点击 <A 
href="#I_get_tons_of_hashfails_on_my_torrents_and_the_torrent_never_finishes">这里</A> 
查看更多信息.</P>
<H3><A name=What_does_availability_mean.3F></A>健康度是什么意思?</H3>
<P>这个数值告诉你与所有开始该任务的用户之间有多少可用的文件副本. 如果这个数值小于 1, 则很有可能无法完成任务的下载. 唯一的办法是等待并祈祷有该任务的种子出现以完成下载. 
你也可以尝试到发布论坛上发贴请求再做种. </P>
<H3><A name=What_does_Force_Start_do.3F></A>强制开始是什么意思 (正在下载 [强制]/正在做种 [强制])?</H3>
<P>强制开始是忽略队列设置和做种优先级设置的一种下载方式. 它也可以用于在计划任务中 "关闭" 时段强制开始下载. 比如, 如果你设置了一次最多有两个任务同时运行, 但是想运行第三个任务时, 就可以强制开始第三个任务. 强制开始的任务不算在队列设置的数目中. 或者如果你将任务设定为分享率达到 150% 时停止, 但想让一个任务达到设定分享率时不停止, 强制开始它就可以达到目的了. 
<br>要强制开始一个任务只需右键点击任务, 选择 "强制开始" 即可. 不管任务的状态是停止, 队列, 还是开始都可以强制开始. 想设置为正常模式时, 右键点击任务任务选择 "开始" 即可. </P>
<H3><A name=What_does_Bandwidth_Allocation_do.3F></A>带宽分配功能有何作用?</H3>
<P>µTorrent 的这个功能可以给任务分配更多或更少的带宽. 它只影响上传, 且只在限制了上传速度时才有作用. </P>
<H3><A 
name=How_is_the_share_ratio_shown_for_torrents_that_are_started_fully_complete.3F></A>任务的分享率是什么意思? 如何计算?</H3>
<P>分享率以 0.000 而不是以 ∞ 开始, 它是对已分发文件大小占任务总大小比例的一个估计. 计算方式为: 已上传文件/文件总大小. </P>
<H3><A name=What_is_the_Logger_tab_and_what_does_it_do.3F></A>日志标签页有何作用?</H3>
<P>日志标签页中包含了程序运行时的状态信息. 当出现错误时, 标签页会显示哈希校验失败和所在的任务, 并且还会记录 UPnP 状态信息. 
点击鼠标右键可以启用 "显示用户流量" (选择 "详细信息" 显示更多信息), 但一般都是一些无用的信息, 所以不推荐启用该功能. 
显示 DHT 节点信息/跟踪信息两个选项是有关 DHT 的一些详细信息. 
点击鼠标右键选择保存日志为文件则可以将日志保存为文件. 需要指定完整的保存路径. 这个功能不会保存缓存中的日志而只保存你启用日志功能后在窗口中看到的一行日志信息. 
要停止记录日志, 请再次选择 "保存日志为文件", 清空文件名称框后点击确定按钮即可.  </P>
<H3><A name=What_is_that_magnifying_glass_and_box_for.3F></A>工具栏上的放大镜图标是什么? 有何作用?</H3>
<P>这是 µTorrent 的搜索栏. 点击放大镜图标可以选择你想使用的搜索引擎, 然后键入你想搜索的内容再按 Enter 键. 搜索结果会使用本机默认浏览器自动打开. 可以在 高级选项 -> 用户界面 的搜索引擎编辑框中添加或删除搜索引擎. 
</P>
<H2><A name=Features></A>功能</H2>
<H3><A name=Does_.C2B5Torrent_support_Protocol_Encryption.3F></A>µTorrent 支持协议加密以对抗 ISP 封锁 P2P 软件吗?</H3>
<P>
支持, 从 1.4.1 beta build 407 版本开始支持此功能并与 Azureus 2.4.0.0 和 BitComet 0.63 保持兼容.
协议加密 (PE) 是 Azureus 与 µTorrent 共同遵循的规范, 它主要用来突破 ISP 对 BitTorrent 协议的嗅探和阻止.
在参数设置的任务中, 你可以选择实施协议加密的模式. 以下是你可以选择的各种模式的解释: 
<UL>
<LI>禁用加密: 不对连出连接进行协议加密, 但接受加密的连入连接.</LI>
<LI>启用加密: 对连出连接进行协议加密, 但如果连接失败则自动转回不加密模式. </LI>
<LI>强制使用加密: 对连出连接进行协议加密, 但如果连接失败不会转向不加密模式.(不推荐)</LI>
<br>"接受未加密的连入连接": 启用或禁用接受未加密的连入连接. 建议选中这个选项.
<br>注意: 所有模式都接受加密的连入连接 (且加密方式可以是双向的)!
</UL></P>
<H3><A name=Does_.C2.B5Torrent_support_DHT_or_Peer_Exchange.3F></A>µTorrent 支持 DHT 网络或用户来源交换吗?</H3>
<P>从 1.2 版本开始, µTorrent 支持连接 DHT 网络. 从 1.4.1 beta 407 版本开始, 支持用户来源交换(当前只与 µTorrent 客户端兼容). 
在种子文件包含私有标识时则会禁用上述功能. 私有标识会使 DHT 网络和用户都不能进行来源交换.<br>
<B>µTorrent 不会支持 tracker 服务器响应信息中包含私有标识的种子! 因为这是非常不安全且不可信的. 
只有内嵌私有标识的种子方被支持.</B><br>
查看 <A href="#What_is_DHT.3F">什么是 DHT 网络?</A> </P>
<H3><A name=Does_.C2.B5Torrent_have_a_plugin_system.3F></A>µTorrent 拥有类似 Winamp 的插件系统吗?</H3>
<P>没有. 不会添加这一功能, 对 µTorrent 来说这是毫无必要的.</P>
<H3><A 
name=Does_.C2.B5Torrent_support_UNC-style_paths_.28e.g._.2F.2F192.168.1.2.2FC.24.2F_.29_.2F_network_drives.3F></A>µTorrent 支持网络路径 (如 \\192.168.1.2\C$\ ) 或网络驱动器吗?</H3>
<P>支持, 从 1.3.1 beta build 374 版本开始支持此功能. 当然, 保存到网络驱动器时目标驱动器的碎片率会增高, 因为无法通过网络进行预先分配磁盘空间的操作. </P>
<H3><A name=Does_.C2.B5Torrent_have_Unicode_support.3F></A>µTorrent 支持 Unicode 吗?</H3>
<P>支持, 从 µTorrent 1.2.3 beta 356 开始支持此功能. 相同的功能可用于 9x/ME 系统, 但 Unicode 本身只于 NT3.51 及更高版本的系统中可用 (2000/XP/2003 是基于 NT 内核的).  </P>
<H3><A name=Does_.C2.B5Torrent_support_multi-tracker_torrents.3F></A>µTorrent 支持多 Tracker 服务器的任务吗?</H3>
<P>支持. µTorrent 当前支持同时连接所有指定的 Tracker 服务器. 
要查看种子的 Tracker 服务器, 只需鼠标右键点击任务然后选择属性即可. 用空行分隔的 Tracker 服务器表明它们在不同的服务器组 (<A 
href="http://wiki.depthstrike.com/index.php/P2P:Protocol:Specifications:Multitracker">查看多 tracker 服务器相关信息</A>) </P>
<H3><A 
name=Does_.C2.B5Torrent_support_UPnP_.28Universal_Plug.27n.27Play.29.3F></A>µTorrent 支持 UPnP (通用即插即用) 吗?</H3>
<P>支持. 从 1.5.1 beta build 462 版本开始支持此功能, 且支持所有操作系统. 
点击菜单 选项 -> 参数设置 -> 连接, 选中 "启用 UPnP 端口映射" 和 "启用 NAT-PMP 端口映射" 即可. 
如果 UPnP 工作正常且接受到连入连接, 就可以在 "日志" 标签页中查看到映射的端口. 
在网络设置向导中, 点击 "测试端口转发设置是否正确" 按钮可以测试 UPnP 是否正常工作. 
如果任务开始一段时间后状态栏的网络状态图标仍然没有变为绿色<IMG alt="green network light" 
src="images/greennetwork.png">或提示无法映射 UPnP 端口, 很可能是因为防火墙阻止连入连接造成的. <A href="#What_does_Unable_to_map_UPnP_port_to_mean.3F">点击这里</A> 获取有关 UPnP 出错的更多信息. 
如果上述方法都无法解决问题则需要手动转发端口. 点击查看 <A 
href="#How_do_I_forward_ports.3F">如何转发端口</A>? </P>
<H3><A name=Does_.C2.B5Torrent_support_RSS_feeds.3F></A>µTorrent 支持 RSS 吗?</H3>
<P>支持, 从 1.3.1 beta build 374 版本开始支持此功能. 注意: 使用 1.4 版本的用户升级到 1.4.1 beta 405 及其后续版本时必须重置 RSS 过滤器. <A 
href="http://www.utorrent.com/rsstutorial.php">点击查看 RSS 功能使用教程(英文)</A> </P>
<P>BTW: 感谢 TVTAD 制作的 RSS 图标.</P>
<H3><A name=Does_.C2.B5Torrent_support_Super_Seeding_mode.3F></A>µTorrent 支持超级种子模式吗?</H3>
<P>支持. 打开任务属性对话框并选中 "初始做种" 项即可. 所谓超级种子模式, 即是 <A 
href="http://en.wikipedia.org/wiki/Super-seeding">点击这里查看</A>. <BR>请注意, 超级种子模式只能应用于只有你一个人在做种且有 2 个以上用户下载的情况下.
如果你的上传速度很快则无需使用这一功能.</P>
<H3><A name=Does_.C2.B5Torrent_support_endgame_mode.3F></A>µTorrent 支持 Endgame 模式吗(用于在下载接近完成时快速完成)?</H3>
<P>支持, 当剩余数据块将要完成时会自动进入这一模式. 该模式可使文件的最后数据块下载得比正常情况下更快. </P>
<H3><A name=Does_.C2.B5Torrent_allow_selective_file_downloading.3F></A>µTorrent 支持选择性的下载任务中的某些文件吗?</H3>
<P>支持, 在添加任务时选择要下载的文件或鼠标左键点击任务然后点击 "文件" 标签页. 鼠标右键点击不需要下载的文件然后选择 "取消下载" 即可.</P>
<H3><A name=Does_.C2.B5Torrent_support_Manual_Announce.3F></A>µTorrent 支持手动连接服务器吗?</H3>
<P>支持, 鼠标右键点击任务然后选择 "手动连接服务器". </P>
<H3><A 
name=Does_.C2.B5Torrent_support_HTTPS_.28SSL.29_or_UDP_trackers.3F></A>µTorrent 支持 HTTPS (SSL) 或 UDP Tracker 服务器吗?</H3>
<P>µTorrent 1.6 支持 SSL 连接(对服务器和 RSS), 但不支持 UDP Tracker 服务器. 不准备支持 UDP tracker 服务器, 因为它缺陷太多. </P>
<H3><A name=Does_.C2.B5Torrent_support_trackerless_torrents.3F></A>µTorrent 支持无服务器/无种子下载吗?</H3>
<P>支持. 种子创建者也可以制作无 Tracker 服务器的种子. </P>
<H3><A name=Does_.C2.B5Torrent_have_an_embedded_tracker.3F></A>µTorrent 拥有内嵌 Tracker 服务器吗?</H3>
<P>有. 在高级选项中启用 bt.enable_tracker 即可. 当然, 这是一个非常普通的服务器, 没有网络界面, 甚至无法看到哪些任务正在下载. 它不适合对安全性有一定要求和下载任务很大的用户. 单机服务器功能较为强大, 只需激活就可以查看所有外部和内部任务. 
一旦激活, 只需简单地将服务器的地址加入任务就可以使用. 服务器的地址一般是 http://你的 IP 地址:端口/announce (端口是 µTorrent 的连入连接端口). IP 地址可以是外网或内网地址, 取决于你想在局域网还是 Internet 上使用它.</P>
<H3><A name=Does_.C2.B5torrent_support_multi-scrape.3F></A>µTorrent 支持多次查询种子进行状态吗?</H3>
<P>支持, 从 1.3.1 beta build 374 版本开始支持此功能. 程序会自动检测不支持多次查询种子进行状态的服务器, 并把该服务器设为查询一次种子进行状态的模式. 
</P>
<H3><A name=Does_.C2.B5Torrent_have_a_boss_key.3F></A>µTorrent 支持老板键功能吗?</H3>
<P>支持, 从 1.3.1 beta build 374 版本开始就支持该功能. 老板键功能就是指按下特定的快捷键就会隐藏或显示客户端窗口和系统托盘图标的功能.</P>
<P>你可以点击菜单 选项 -> 参数设置 -> 其它选项, 然后在老板键后的编辑框中按下你想要设置的按键. 无法指定单独的字母和数字作为快捷键, 必
须与 Alt, Ctrl, 或 Shift 键联用. 注意: F 键可以单独使用, 但是不推荐这样设置. 这个热键是全局热键, 可能会与其它程序的热键冲突, 请谨慎设置.
要取消老板键功能, 点击编辑框并按下 Del 键或 Backspace 键即可. </P>
<H3><A 
name=Does_.C2.B5Torrent_automatically_ban_peers_after_a_certain_number_of_hashfails.3F></A>µTorrent 支持在对获得的数据校验失败达到一定数量时自动阻止相应用户吗?</H3>
<P>支持, 在 5 次数据校验失败后, µTorrent 会阻止并排除相应用户. 如果要解除某个任务对用户的阻止, 鼠标右键点击该任务, 选择高级 -> 重置.  </P>
<H3><A 
name=Does._C2.B5Torrent_have_a_BitComet_style_add_torrent_dialog.3F></A>µTorrent 拥有类似 BitComet 式样的添加任务对话框吗?</H3>
<P>是的. 如果你选中了 "下载文件保存到下列文件夹中:" 那么还需要选中 "始终显示添加任务对话框" 选项或点击菜单 文件 -&gt; 添加任务(非默认保存位置).
 在这个对话框中你可以编辑任务属性, 选择文件, 设置标签, 选择保存目录, 跳过校验检查以及添加任务时保持停止状态. </P>
<H3><A name=Does.C2.B5Torrent_have_a_web_interface.3F></A>µTorrent 拥有网络接口吗(支持远程控制)?</H3>
<P>支持. <A href="http://forum.utorrent.com/viewtopic.php?id=14565">请点击这里查看详细信息</A>.</P>
<H3><A 
name=Can_you_make_.C2.B5Torrent_automatically_run_a_program_after_the_download_finishes.3F></A>如何使 µTorrent 在下载任务完成后自动运行某个程序?</H3>
<P>右键点击任务列表中的相应任务选择 "属性", 在弹出对话框的 "高级" 标签页面中可以进行设置. </P>
<H3><A name=Is_there_any_foreign_language_support_for_.C2.B5Torrent.3F></A>µTorrent 支持多国语言吗?</H3>
<P>支持, 但是使用语言文件无法完全汉化, 建议使用汉化版. </P>
<H2><A name=Installation></A>安装</H2>
<H3><A name=Does_.C2.B5Torrent_install_itself.3F></A>μTorrent 支持自动安装吗?</H3>
<P>不支持, 它是一个独立的应用程序, 除非你使用安装版本. 当然, 第一次运行时会询问是否要在开始菜单和桌面上创建快捷方式. </P>
<H3><A name=Where_are_the_settings_and_.torrent_files_stored.3F></A>参数设置和种子文件保存到哪个位置?</H3>
<P>默认保存在安装目录下, 建议在参数设置中为种子文件单独设置保存位置. </P>
<H3><A name=How_do_I_uninstall_.C2.B5Torrent.3F></A>如何卸载 μTorrent?</H3>
<P>建议使用汉化版的卸载程序进行卸载, 这是最彻底的卸载方法. </P>
<H3><A 
name=How_can_I_use_.C2.B5Torrent_on_a_USB_key_or_other_removable_drive.3F></A>如何在 U 盘或其它可移动存储设备中使用 μTorrent?</H3>
<P>复制安装后的文件夹到移动存储设备中即可. </P>
<H3><A name=How_can_I_share_my_torrents_between_user_profiles.3F></A>如何在多用户环境中使用 μTorrent?</H3>
<P>如果在多用户环境下使用它, 所有用户应该都享有程序所在文件夹的存取权限, 这样他们才能共享参数设置并能续传数据. </P>
<H3><A name=How_can_I_backup_my_settings.3F></A>如何备份我的参数设置?</H3>
<P>复制程序所在文件夹中的所有内容即可, 注意: 文件关联无法备份. </P>
<H3><A name=How_can_I_reset_the_settings_back_to_the_defaults.3F></A>如何重置所有参数为默认值?</H3>
<P>退出 μTorrent 程序, 删除程序所在文件夹中的 settings.dat 和 settings.dat.old 即可. </P>
<H3><A 
name=How_can_I_change_the_system_tray_icon_.2F_GUI_icon_for_.C2.B5Torrent.3F></A>如何更改系统托盘图标或程序界面图标?</H3>
<P>将你喜欢的图标改名为 tray.ico (系统托盘图标) 或 main.ico (主界面窗口图标) 置于 settings.dat 所在目录中重新运行程序即可. 你可以在 <A href="http://www.utorrent.com/skins.php">皮肤页面</A> 以及论坛的 <A href="http://forum.utorrent.com/viewforum.php?id=6">用户界面设计</A> 板块找到相关资源和教程. </P>
<H3><A name=Can_I_change_.C2.B5Torrent.27s_skin.3F></A>μTorrent 支持皮肤功能吗?</H3>
<P>请访问 <A href="http://www.utorrent.com/skins.php">皮肤页面</A> 以及论坛中的 
<A href="http://forum.utorrent.com/viewforum.php?id=6">用户界面设计</A> 板块. 
要使用皮肤, 只需要将其置于 Settings.dat 文件所在目录并重新运行程序即可. </P>
<H2><A name=Network></A>网络</H2>
<H3><A 
name=Does_.C2.B5Torrent_work_well_on_Windows_XP_SP2_systems_with_an_unpatched_TCPIP.SYS.3F></A>μTorrent 可以在没有修改并发连接数的 Windows XP SP2 系统上正常工作吗?</H3>
<P>可以, 没有打过破解补丁的 Windows XP SP2 系统的半开连接数最大只能为 10, 从 1.2 版本开始 μTorrent 的 net.max_halfopen 
的值默认为 8.
将 TCPIP.SYS 并发连接数的值改为较大的数值有助于解决无法连接 Tracker 服务器等问题, 但是可能会导致某些型号的路由器出现假死. 
如果你想破解 TCPIP.SYS, 可以用 LvlLord 的提供的 <A href="http://www.lvllord.de/?lang=en&amp;url=downloads">EventID 4226</A> 来修改. 
注意: 设置的过大可能会使下载时的网页浏览速度减慢! 另外请注意 net.max_halfopen 的数值应该总是低于所修改的 TCPIP.SYS 半开连接数的数值. </P>
<H3><A 
name=Help.21_.C2.B5Torrent_is_sending_e-mails.2Faccessing_the_web.2Fetc.21></A>求助! 防火墙或反病毒程序报告 μTorrent 正在发送电子邮件或访问网络!</H3>
<P>出现这一问题 99% 是误报. 有时客户端使用保留端口如 25, 80 等是为了突破 ISP 对 BT 协议的封锁. 你应该允许这一行为, 不要阻止! 建议关闭防火墙/杀毒软件的电子邮件保护, 或者把 "utorrent.exe" 添加到排除列表中. 
还有一种情况, 那就是在启动时 μTorrent 会根据设置从 uTorrent.com 加载页面以检查有无最新版本. 你也应该允许这一行为. 这一功能也可以在 参数设置 -> 常规 中禁用: 取消选中 "自动检查软件更新" 即可. 

如果启用了 DHT 网络连接, 程序也会访问 router.utorrent.com 网站. 这对 DHT 网络连接是必需的 (至少在第一次启动时). 如果你阻止了它, DHT 网络连接就不可用!

<B>简单说来就是你的防火墙出现误报, 你无需理会. ^_^</B></P>
<H3><A 
name=My_firewall_is_reporting_connections_being_made_by_.C2.B5Torrent_on_a_port_besides_the_one_I_selected._What_gives.3F></A>防火墙程序报告 μTorrent 使用的端口不是我所选择的端口. 这是什么原因?</H3>
<P>只有连入连接会使用 μTorrent 中设置的端口, 而连出连接使用的是本机的随机端口; 这是 TCP/IP 实现功能的方式, 不是程序本身的问题. 
如果你安装了防火墙, 你必须允许所有基于 TCP 和 UDP 的连出连接才能使程序正常运行.
</P>
<H3><A 
name=How_do_I_change_the_number_of_connections_.C2.B5Torrent_uses.3F></A>如何更改 μTorrent 使用的连接数?</H3>
<P>点击菜单 选项 -> 参数设置 -> 任务, 编辑 "全局最大连接数" 和 "每任务的最大连接数" 的数值.<BR><A 
href="#connection_note">点击这里查看如何设置</A> </P>
<H3><A name=What_do_the_Network_Status_lights_mean.3F></A>状态栏的网络状态图标有何含义 (<IMG alt="green network light" 
src="images/greennetwork.png">, <IMG alt="yellow network light" 
src="images/yellownetwork.png">, <IMG alt="red network light" 
src="images/rednetwork.png">)?</H3>
<P>绿色图标 <IMG alt="green network light" src="images/greennetwork.png"> 表示程序工作正常 (端口转发成功并且有连入连接)<BR>
黄色图标 <IMG alt="yellow network light" 
src="images/yellownetwork.png"> 当前没有连入连接. 如果始终是黄色图标, 则表示你的端口可能没有转发成功. 建议使用网络设置向导中的端口转发功能进行检查. 
如果端口检查页面中显示 "xxx OK" 的字样, 则表示端口转发成功.<BR>红色图标 <IMG alt="red network light" 
src="images/rednetwork.png"> 表示 µTorrent 无法绑定监听端口. 这一般是因为防火墙程序阻止了连接或是其它程序占用了设置的监听端口, 请尝试配置防火墙或关闭其它网络程序. </P>
<H3><A name=Why_are_my_torrents_going_so_slow.3F></A>为什么我的下载速度始终很慢?</H3>
<P>一般来说, 这是因为 µTorrent 使用的端口没有被路由器转发. 如果 µTorrent 状态栏显示图标 <IMG alt="yellow network light" src="images/yellownetwork.png">, 
就可以确定是这个原因. 出现这种情况还可能是因为防火墙阻止了 µTorrent 的连接. 请检查是否已经在使用的防火墙中为 µTorrent 
配置了允许连接的规则! 如果你的防火墙阻止了 ICMP 数据包 (Windows 防火墙默认阻止 ICMP 数据包), 你应该允许 "目标无法到达" 以避免 DHT 网络连接无法正常工作. <BR>
如果所有设置正确且程序正常工作, 你可以在状态栏上发现图标 <IMG alt="green network light" 
src="images/greennetwork.png">  (位于 DHT 网络状态左侧). 如果仍然为黄色图标, 请仔细检查设置并稍候片刻.<BR><BR>如果你是用的是 Windows XP SP1 或 SP2 自带的防火墙, 请确认 <A 
href="http://support.microsoft.com/kb/283673">已经关闭 Internet 连接防火墙 (SP1) 或 Windows 防火墙 (SP2)</A>. <BR>建议直接禁用 Internet 连接防火墙 (ICF) 或 Windows 防火墙 / Internet 
连接共享 (ICS) 服务以避免其与第三方防火墙软件发生冲突. 你可以在 控制面板 -&gt; 管理工具 -&gt; 服务 中找到并禁用相关服务.<BR><BR>如果你使用其它防火墙并且已经配置了端口转发(或没有使用路由器)却仍然无法正常连接,
 请尝试 <B>卸载</B> 防火墙或禁用它! 如果你没有使用路由器和防火墙却仍然无法正常连接, 请尝试配置你的调制解调器. </P>
<P>你也许想关闭程序的 UPnP 功能而在路由器上转发你所设置的端口, 因为某些路由器的系统的 UPnP 功能不够规范或实现的不完全. 请确认你的路由器/调制解调器不在 <A 
href="#Modems_routers_that_are_known_to_have_problems_with_P2P">这个列表</A> 中!</P>
<P><A name=connection_note></A><B>你是否已经使用了网络设置向导功能并且已经阅读了提供的建议? 你的上传带宽选择的是否正确? 一般来说 1MB 带宽以下的 ADSL 请选择 128k - 256k 的上行带宽, 2MB 带宽以上的 ADSL 请选择 256k - 512k 的上行带宽, 更高的网络带宽自己看着办. ^_^
</B> 注意不要把上传连接数或每个任务的连接数设置的过高. 注意同一时间尽量不要开始多于 3 个任务. 一般来说, 在你同时运行多个任务时, 2MB ADSL 
连接(上行 512k) 的每个任务的连接数不要超过 200, 且上传连接数不要超过 5. 当然这些数值还是取决于你的网络带宽的. 并不是所有的值都是设置越高速度就越快, 相反可能会减慢你的下载速度.</P>
<P>推荐破解 TCPIP.SYS 的半开连接数, 并且高级选项中的 net.max_halfopen 参数的数值务必 <B>低于</B> 半开连接数! 注意经常检查 TCPIP.SYS 半开连接数, 因为 Microsoft 
的更新中经常会覆盖这一文件, 这是你必须重新对该文件进行破解.</P>
<H3><A name=How_do_I_forward_ports.3F></A>如何转发端口?</H3>
<P>这个问题超出了本篇帮助的范围, 请自行网上搜索你使用的路由器型号及转发设置. 最少需要支持进行 TCP 转发, UDP 转发一般用来支持 DHT. 
请先运行某个下载或上传的任务. (如果你的系统是 Windows XP/2003 或你使用的是 1.5.1 beta 462 及其后的支持所有操作系统 UPnP 的版本, 而且你已开启路由器的 UPnP 功能, 你可以启用程序内置的 UPnP 功能进行自动转发端口. 当然这个在一定程度上存在安全隐患) 如果状态栏上出现图标 <IMG alt="yellow network light" 
src="images/yellownetwork.png">, 请检查你的防火墙设置并放行 μTorrent 的连入连接. </P>
<H3><A name=What_ports_should_I_use_for_.C2.B5Torrent.3F></A>μTorrent 工作时使用哪个端口?</H3>
<P>建议不要使用 6881-6889 范围内的任何端口. 另外, μTorrent 只使用一个监听端口, 所以你只需要转发一个端口即可. 建议使用高于 10000 的端口. </P>
<H3><A name=How_do_I_change_the_port_.C2.B5Torrent_uses.3F></A>如何更改 μTorrent 使用的端口?</H3>
<P>点击菜单 选项 -> 网络设置向导. 在 "当前使用端口" 后的编辑框中输入你想要使用的端口号然后确定即可. 
你也可以点击菜单 选项 -> 参数设置 -> 连接, 在 "本机使用端口" 后的编辑框中输入你想使用的端口号然后点击确定.
当然, 设置完后需要重启 μTorrent 才可生效. 确保你所设置的端口没有被其它程序占用! </P>
<H3><A name=Does_.C2.B5Torrent_support_proxies.3F></A>μTorrent 支持代理服务器吗?</H3>
<P>支持. 点击菜单 选项 -> 参数设置 -> 连接. μTorrent 支持 SOCKS4, SOCKS5, HTTP 连接和 HTTP 等类型的代理服务器. 
客户端通信代理支持 SOCKS4, SOCKS5 和 HTTP 连接. HTTP 连接是允许任意 TCP 连接的一种 HTTP 代理服务器. 你需要根据代理服务器的类型自行选择并进行设置.</P>
<H3><A 
name=Does_.C2.B5Torrent_have_a_scheduler_to_limit_download_and_upload_speeds.3F></A>μTorrent 支持使用计划任务限制下载和上传速度吗?</H3>
<P>支持, 在参数设置对话框中可以设置. 目前允许设置上传/下载速度, 限速, 全速或调整程序在每天各时段里占用的网络带宽. 还可设置某些时段禁止连接 DHT 网络. 
1.4.1 beta 417 及其后续版本支持做种时使用计划任务模式. 选择时段数值时按住 Shift 键则计划任务列表会变为红色, 此时的设置就是做种模式的计划任务.
</P>
<H3><A 
name=How_can_I_make_.C2.B5Torrent_use_a_different_upload_speed_when_seeding.3F></A>如何设置 μTorrent 在做种时使用不同的上传速度?</H3>
<P>点击菜单 选项 -> 参数设置 -> 连接, 选中 "下载速度为零时上传速度限制为", 在后面的编辑框中输入你想要限定的速度数值即可.</P>
<H3><A 
name=How_can_I_make_.C2.B5Torrent_report_a_different_IP_to_the_tracker.3F_I.27m_behind_a_proxy_and_need_this_function.></A>如何使 μTorrent 在客户端的 IP 地址更改时向服务器报告 IP 地址? 我使用了代理服务器所以需要这一功能.</H3>
<P>点击菜单 选项 -> 参数设置 -> 任务, 在 "提交到服务器的 IP 地址/主机名" 后的编辑框中输入所要报告的 IP 地址. 从 1.3.1 beta build 374 以后的版本开始同时支持 IP 地址和域名. 启用 NAT 转发时无需再使用此功能, 这种情况下服务器会自动获取客户端 IP 地址. 只有当你的外网 IP 地址 (实际网络连接的 IP 地址) 与服务器上查询到的地址不一致时才使用这一功能. 另外, 某些服务器会忽略这个指定的值, 那么这个功能就没什么用了. </P>
<H3><A 
name=How_can_I_make_.C2.B5Torrent_use_a_specific_network_adapter.3F></A>如何使 μTorrent 使用指定的网卡工作?</H3>
<P>点击菜单 选项 -> 参数设置 -> 高级选项, net.bind_ip 指定哪个网卡用于连入连接, 而 net.outgoing_ip 指定哪个网卡用于所有的连出连接, 在编辑框中输入你想使用的网卡的 IP 地址即可. 本选项一般用于多网卡的计算机中, 单网卡用户可以忽略设置.</P>
<H3><A 
name=How_can_I_change_the_number_of_active_torrents.2Fdownloads.3F></A>如何设置最多同时进行的任务数和下载任务数?</H3>
<P>点击菜单 选项 -> 参数设置 -> 队列, 更改 "最多同时进行的任务数 (上传与下载)" 和 "最多同时进行的下载任务数" 其后编辑框中的数值即可. </P>
<H2><A name=Error_Messages></A>错误消息</H2>
<H3><A 
name=While_torrenting_I_get_error_the_system_cannot_find_the_path_specified></A>下载或做种时 μTorrent 提示 "错误: 系统没有找到指定的路径" 然后挂起任务.</H3>
<P>这个问题是由于 Windows 操作系统对路径的长度存在限制. Windows 规定路径的长度最大为 255 字符 (包括文件名称在内). 建议尽量将种子文件保存路径设置为最接近驱动器根目录的文件夹, 例如 D:\Torrent. </P>
<H3><A name=How_do_I_fix_Error_unable_to_save_the_resume_file.3F></A>如何解决 "错误: 无法保存续传信息文件"?</H3>
<P>很简单, 在主程序所在的文件夹中创建两个名为 "settings.dat" 和 "resume.dat" 的文件即可. </P>
<H3><A name=What_does_Download_Limited_in_the_status_bar_mean.3F></A>状态栏中的 "下载已被限制" 是什么意思?</H3>
<P>这是 µTorrent 内置的反吸血功能. 出现这一提示意味着用户的上传速度限制过低, 程序将自动把下载速度强制设为上传速度的 6 倍. 请勿设置上传速度低于 5 KiB/s. 
</P>
<H3><A name=What_does_Disk_Overload_in_the_status_bar_mean.3F></A>状态栏中的 "磁盘负荷过大" 是什么意思?</H3>
<P>这是因为硬盘无法跟上数据的读/写速度, 常见于高速网络下载时. 要解决这一问题, 你可以更改 <A 
href="#How_do_I_modify_the_disk_cache_options.3F">磁盘缓存</A> 的大小和其它选项. <BR>
如果是在添加任务时出现这一提示, 你可以忽略它. 它会在几分钟后自动消失, 不会对硬盘或下载有何影响. </P>
<H3><A name=What_does_Unable_to_map_UPnP_port_to_mean.3F></A>"无法映射 UPnP 端口到 xx.xx.xx.xx:xx" 等类似提示是什么意思?</H3>
<P>出现这个错误消息表明 μTorrent 无法映射 UPnP 端口. 如果状态栏标识为绿色或者你已经手动设置路由器转发指定的端口, 则可以忽略这条消息. 但是, 如果状态栏标识为黄色/红色, 则意味着你需要手动转发端口, 或者你的防火墙正在阻止连入连接. μTorrent 从 1.5 版本开始支持 Windows XP 环境下的 UPnP 功能, 从 1.5.1 beta 462 开始支持所有操作系统下的 UPnP 功能. </P>
<H3><A name=What_does_Error_opening_Windows_Firewall_mean.3F></A>"打开 Windows 防火墙出错, 代码: 0x800706D9" 是什么意思?</H3>
<P>出现这一提示说明 µTorrent 无法使用 Windows 防火墙的 API 将自身添加到 Windows 防火墙的例外列表中. 这一般是由于系统没有安装 Windows 防火墙或防火墙被用户禁用. 你完全可以忽略这个提示. 当然, 如果你安装了其它软件防火墙, 请手动配置它允许 µTorrent 的连入和连出连接. </P>
<H3><A name=I_get_Error_Not_enough_free_space_on_disk_when_I_have_enough_free_space></A>μTorrent 提示 "错误: 磁盘剩余空间不足." 但是我确信磁盘空间足够!</H3>
<P>这种情况只会出现在 FAT32 格式的驱动器上, 因为 FAT32 格式对单个文件的大小存在限制. FAT32 格式的驱动器上无法创建超过 4GB 大小的文件, 如果你下载的是 DVD 镜像或大于 4GB 的单个文件则会出现错误提示. 你可以将该驱动器转换为 NTFS 格式或使用已有的 NTFS 格式驱动器来保存文件. </P>
<P>要转换驱动器为 NTFS 格式, 点击 开始 -> 程序 -> 附件 -> 命令提示符, 然后输入 
convert X: /FS:NTFS(X 为你要转换的驱动器的盘符). </P>
<H3><A name=Error_Custom_001></A>μTorrent 在与服务器通讯时假死且提示 "向一个无法连接的网络尝试了一个套接字操作."</H3>
<P>请 <A href="#My.C2.B5Torrent_keeps_freezing_on_certain_trackers">点击这里</A> 查看.</P>
<H3><A name=Error_Custom_002></A>μTorrent 提示 "错误: 访问被拒绝" 或 "错误: 进程无法访问文件, 该文件正被另一进程使用" 然后自动挂起任务!</H3>
<P>请 <A href="#I_get_Error_Access_Denied_and_.C2.B5Torrent_halts_the_torrent">点击这里</A> 查看. </P>
<H3><A name=Error_Custom_003></A>μTorrent 提示 "无法执行套接字操作, 系统缓冲空间不足或队列已满" 然后失去响应.</H3>
<P>请 <A 
href="#I_get_a_socket_buffer_space_error_and_the_client_halts">点击这里</A> 查看. </P>
<H3><A name=I_get_Error_Element_not_found_and_the_torrent_stops></A>μTorrent 提示 "错误: 没有找到组件" 然后挂起任务.</H3>
<P>这通常是因为启用了 bt.compact_allocation 功能. 请将它的值设置为禁用, 这个功能尚未完全开发完成. 如果你想在下载时节省磁盘空间, 请启用 diskio.sparse_files 功能(仅适用于 NTFS 格式分区) 来代替它. 这一提示也可能在从一个任务中移动或重命名文件时出现. 建议重新添加, 重命名文件或重新检查完整性. </P>
<H3><A name=I_get_Error_Parameter_is_incorrect_when_selectively_downloading></A>μTorrent 提示 "错误: 参数不正确" (运行于 Windows 95/98/ME 系统中并进行选择性下载时)</H3>
<P>我们正在查找出现这个问题的原因. 这个出错提示仅仅出现在使用 Windows 95/98/ME 并进行选择性下载时, 但是只要你重新开始任务就没有问题了. </P>
<H3><A name=I_get_Error_Data_Error_cyclic_redundancy_check></A>我遇到提示消息 "错误: 数据错误 (循环冗余检查)" 然后 μTorrent 挂起任务.</H3>
<P>这并不是 μTorrent 出错, 而是你的硬盘出现了错误. 出现这个提示意味着无法向硬盘写入或读取数据, 这可能是因为扇区不完整或者被损坏, 请及时检查, 修复或更换硬盘. <br>建议运行磁盘检查工具进行检查恢复.</P>
<H2><A name=Troubleshooting></A>问题</H2>
<H3><A name=Why_is_my_torrent_stuck_at_a_certain_percent.3F></A>为什么任务进度始终停止在某个百分比上不再上升?</H3>
<P>请点击链接: <A 
href="#What_does_availability_mean.3F">健康度是什么意思?</A> </P>
<H3><A 
name=Why_doesn.27t_.C2.B5Torrent_report_me_as_a_seeder_when_selectively_downloading.3F></A>为什么进行选择性下载并完成后 μTorrent 不将我报告为种子?</H3>
<P>按照定义, 种子是指已经完成下载所有文件的客户端. 换句话说, 即使你确实在做种, 但如果你不是完全下载相应任务中的所有文件, μTorrent 仍然不会将你报告为种子. ^_^ </P>
<H3><A name=Why_does_pause_mode_keep_downloading_or_uploading.3F></A>为什么暂停模式下依然存在下载速度和上传速度?</H3>
<P>下载速度和上传速度降为零是需要花费一段时间的(可能是几分钟), 因为 μTorrent 需要完成发送/接收已有队列中的所有分块. 另外暂停不会断开已有连接, 因为 BitTorrent 协议还有一定的网络开销, 所以在暂停后的几分钟内可能仍然会有一些流量. </P>
<H3><A name=Why_does_it_show_download_speed_when_seeding.3F></A>为什么在做种时仍然有 0.1-0.2 KiB/s 的下载速度?</H3>
<P>µTorrent 此时显示的速度是 BitTorrent 协议的开销; 做种时有少量流量是很正常的(这部分流量是客户端通讯产生的). 
</P>
<H3><A name=Why_is_there_an_ETA_when_seeding.3F></A>为什么在做种时仍然显示剩余时间?</H3>
<P>µTorrent 显示的剩余时间是基于用户设置的做种目标(分享率)和当前上传速度来计算出来的, 这可以很方便的让用户知道还需要上传多长时间就可以达到指定的分享率. </P>
<H3><A name=Why_do_the_up.2Fdown_buttons_not_move_the_torrent.3F></A>为什么上移/下移按钮并不移动任务?</H3>
<P>上移/下移按钮仅仅改变下载任务在队列中的顺序, 并不移动列表中的任务顺序, 换言之并不影响你期望的任务开始的先后顺序. <br>当然你也可以在改变顺序后点击任务列表的 "#" 栏目来使列表根据序号排序即可.
</P>
<H3><A 
name=Why_does_.C2.B5Torrent_create_files_I_set_to_.22Don.27t_Download.3F.22></A>为什么选择了不下载文件 μTorrent 却依然创建了相应文件?</H3>
<P>请将参数设置的高级区段中的 diskio.use_partfile 的值设为启用即可. 
</P>
<H3><A 
name=Why_can.27t_I_see_anything_in_the_directory_browser_dialog.3F></A>为什么目录浏览对话框中什么内容也没有?</H3>
<P>请将参数设置的高级区段中的 gui.compat_diropen 的值设为启用, 这一问题将会在以后的版本中修复. </P>
<H3><A 
name=Why_do_my_torrents_grind_to_a_halt_with_disk_overload_whenever_I_add_a_new_one.3F></A>为什么添加的任务被挂起且 μTorrent 提示 "磁盘负荷过大"?</H3>
<P>这是由于 μTorrent 编写时考虑不周造成的, 这个问题会在以后的版本中解决. 只需要稍微等待几分钟这一提示就会消失, 不会对计算机或下载任务有何影响. </P>
<H3><A name=Why_does_.C2.B5Torrent_use_FixedSys_on_Windows_NT_4.3F></A>为什么在 Windows NT4/98/ME 等系统中 µTorrent 使用 FixedSys 字体?</H3>
<P>因为你的系统中没有安装 Tahoma 字体. </P>
<H3><A 
name=.C2.B5Torrent_won.27t_open_torrent_files_even_though_I_associated_torrents_with_it></A>为什么设置了文件关联后 μTorrent 仍然无法打开种子文件?</H3>
<P>请检查参数设置对话框中常规区段的 "设置为默认的 BT 客户端程序" 按钮处于无法点击的状态. 如果你使用的是 FireFox 浏览器且打开种子文件时 Firefox 提示 "无效的菜单句柄", 请点击 FireFox 菜单栏中的工具 -> 选项 -> 内容 -> 管理, 从列表中移除 TORRENT 项目然后点击确定按钮. 如果资源管理器或其它程序出现这个错误提示, 请鼠标右键点击任意一个种子文件, 选择 打开方式 -> 浏览, 找到 μTorrent 并勾选 "始终使用选择的程序打开这种文件". 如果上述方法都没有效果, 请点击资源管理器菜单栏中的工具 -> 文件夹选项 -> 文件类型, 从列表中移除 TORRENT 项目. 然后再使用 μTorrent 的 "设置为默认的 BT 客户端程序" 功能. </P>
<H2><A name=Misc></A>其它</H2>
<H3><A name=I_found_a_bug.2C_what_do_I_do.3F></A>我发现了程序的一个错误, 我应该怎么办?</H3>
<P>查看你发现的错误是否已经有人报告. 访问 <A 
href="http://forum.utorrent.com/">µTorrent 论坛</A>, 进入 "Found Bugs" 
板块并搜索相应关键字. 如果这个错误还没有被报告, 请注册并发帖报告. 注意先检查一下这个问题不是由 <A 
href="#Incompatible_software">软件兼容性</A> 导致的. </P>
<H3><A 
name=My_question_isn.27t_answered_here_.2F_I_wanna_request_a_feature.2C_is_there_somewhere_I_can_go.3F></A>我没有在这里找到我碰到的问题的答案或是我希望作者开发新的功能, 我应该怎么办?</H3>
<P>你可以在 <A href="http://forum.utorrent.com/">µTorrent 
论坛</A> 获取答案或请求开发新的功能, 但是请先使用搜索功能确定你的问题尚未被人回答或请求尚未有人提出. 否则你的帖子将会被锁定或删除. </P>
<H3><A 
name=Does_.C2.B5Torrent_work_on_a_486_with_Windows_95_and_14MiB_of_RAM.3F></A>µTorrent 可以运行于 486 平台且只拥有 14 兆内存的 Windows 95 系统中吗?</H3>
<P>是的, 只要安装了相应版本的 <A 
href="http://download.microsoft.com/download/0/e/0/0e05231b-6bd1-4def-a216-c656fbd22b4e/W95ws2setup.exe">Winsock2</A> 
更新程序. 可能需要降低界面的刷新周期并且以后台方式运行. </P>
<H3><A name=What_is_DHT.3F></A>什么是 DHT 网络?</H3>
<P>DHT (<A href="http://en.wikipedia.org/wiki/Distributed_hash_table">分布式哈希表, 技术说明</A>) 是一项 BitTorrent 客户端的附加功能, 用来支持无 Tracker 服务器的下载. 具体说来就是即使无法连接到 Tracker 服务器
或是服务器下线, 都不会影响客户端下载. (查看 <A 
href="#Does_.C2.B5Torrent_support_trackerless_torrents.3F">µTorrent 支持无服务器/无种子下载吗?</A>). </P>
<P>µTorrent 的 DHT 功能与 Mainline 和 BitComet 相同, 但是与 Azureus 的功能不兼容. </P>
<H3><A 
name=Why_does_.C2.B5Torrent_show_less_DHT_nodes_in_the_status_bar_than_BitComet_Azureus.3F></A>为什么 µTorrent 状态栏上显示的已连接 DHT 节点数比 BitComet 或 Azureus 少?</H3>
<P>这个数字的大小并不影响下载速度, 仅仅是计算方法的不同而已. 因为 µTorrent 仅显示直接连接到的 DHT 节点的数目. 
BitComet 显示所有包括超过一个中转点的节点数目, 当然会显示较大的节点数. Azureus 则估算 DHT 网络中所有节点/用户数, 数字会更大. </P>
<H3><A 
name=Does_DHT_mean_my_torrents_from_private_trackers_are_getting_leaked.3F></A>使用 DHT 会使来自私有 Tracker 服务器的种子泄露吗?</H3>
<P>如果创建的种子文件带有私有标记, 则该任务的连接 DHT 网络的功能将会被自动禁用. 
你可以通过查看任务属性来检查: 如果 "连接 DHT 网络" 是灰色的, 则 DHT 已被成功禁用. </P>
<P>µTorrent 不支持从 tracker 服务器响应的信息中获取私有种子信息, 因为这样坐是不安全且不可靠的. 
<B>如果你是 tracker 服务器的管理员且想要使私有标识工作正常, 请强制 tracker 服务器对特定的种子文件使用私有标识. BitComet 的种子制作功能并不能正确的生成私有种子, 同样其客户端也不重视私有标识</B> (<A 
href="http://www.slyck.com/news.php?story=1021">点击这里查看相关信息</A>).</P>
<H3><A name=I_don.27t_want_DHT_on_anyway.2C_how_do_I_turn_it_off.3F></A>我不想使用 DHT 网络, 应该如何关闭?</H3>
<P>打开参数设置对话框, 打开任务页面, 取消选中 "启用连接 DHT 网络". 同样, 你也可以取消单个任务的连接 DHT 网络功能: 打开任务属性对话框然后取消选中 "启用连接 DHT 网络" 即可.</P>
<P>还有一个选项可以禁止新任务连接 DHT d网络, 可以让你即使打开了全局应用连接 DHT 网络的功能也无需担心私有种子会通过 DHT 网络泄露出去.</P>
<H3><A name=Can_you_implement_password_locking.3F></A>µTorrent 支持密码锁定功能吗?</H3>
<P>不, 这个功能不在开发计划中. 如果你需要锁定 µTorrent, 请使用 2k/XP/2003 内建的锁定功能(Win+L). </P>
<H3><A name=Can_you_implement_manual_client_banning.3F></A>µTorrent 支持手动阻止客户端吗?</H3>
<P>不, 这个功能不在开发计划中, 这样做是违反 P2P 的分享精神的. 阻止用户的功能还是留给 Tracker 服务器来执行. 如果你需要阻止某个 IP 段, 使用文件 ipfilter.dat 是个不错的选择. </P>
<H3><A name=What_do_all_those_settings_in_Advanced_do.3F></A>高级选项中的各个参数的含义是什么?</H3>
<UL>
  <LI>gui.compat_diropen&nbsp;使用不同的文件夹打开窗口以使那些无法使用默认文件夹浏览窗口查看文件的用户也可正常使用. 
  <LI>net.bind_ip&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;强制 µTorrent 仅使用指定的网卡接受连入连接. 输入要使用的网卡的 IP 地址即可. 
  <LI>net.outgoing_ip&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 强制 µTorrent 使用指定的网卡发送所有连出连接. 输入要使用的网卡的 IP 地址即可. 
  <LI>net.outgoing_port&nbsp;&nbsp; 强制 µTorrent 使用一个端口处理连出连接, 用于某些特殊情况下. 如对某些型号的路由器启用该选项并将该端口设置为与连入端口相同, 都将会减少 NAT 转发规则数目从而有效的减少路由崩溃情况的发生. 仅支持 Win 2003 以上系统. 
  <LI>net.low_cpu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;轻微降低 CPU 的使用率. 关闭此功能有助于提高下载速度. 
  <LI>net.max_halfopen&nbsp;&nbsp;&nbsp;&nbsp;指定 µTorrent 的最大并发连接数. 注意: 在没有打过 TCP/IP 补丁的 XP SP2 系统上设置值不能超过 8! 
  <LI>net.wsaevents&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;用来解决某些莫名其妙的防火墙问题. 你可以逐步降低该值并查看是否有效. 
  <LI>ipfilter.enable 启用或禁用 ipfilter.dat 文件
  <LI>dht.rate&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;指定 DHT 使用的带宽. 设为 -1 意为由程序根据上传速度自动管理. 可设置的值的范围在 512 到 8192 之间. 算法为使用最大上传速度除以 16 获得该值, 比以前的带宽占用 (4096) 低了很多, 提高该值有助于提升 DHT 网络的性能.
  <LI>rss.update_interval&nbsp;&nbsp;设置 RSS 的更新间隔时间. 低于 5 的值将被自动忽略并重置为默认值 5. 
  <LI>gui.update_rate&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;设置程序界面的刷新间隔时间, 单位为毫秒. 最小值为 500. 
  <LI>gui.delete_to_trash 启用后将尽可能的删除文件到回收站. 同样可在右键点击工具栏删除图标时选择 "移动到回收站" 选择. 
  <LI>gui.default_del_action 设置默认的删除操作. 如需修改本选项请右键点击工具栏删除按钮并按住 Shift 键选择想要设置为默认删除操作的菜单项. 
  <LI>gui.bypass_search_redirect 忽略 Nanotorrent 搜索重定向. 
  <LI>queue.dont_count_slow_dl/ul 设置 µTorrent 不对那些速度低于 1KiB/s 的下载或上传连接用户进行计数. 
  <LI>queue.prio_no_seeds 优先对那些网上没有种子的任务进行做种. 
  <LI>bt.scrape_stopped 启用/禁用对已停止任务的种子进行状态查询 (不在队列中). 
  <LI>bt.compact_allocation 允许 µTorrent 按照 Python 式样创建文件以节省空间. 这个选项不能与 partfile 功能同时使用. 
	这个功能有时会无法正常工作: 如果你遇到提示 
  "没有找到组件&quot; 就说明该功能出问题了. 
  <LI>bt.enable_tracker 启用或禁用内置的 tracker 服务器. tracker 服务器地址为 <a href="http://你的">http://你的</a>公网 IP 地址:端口/announce (不支持域名). 
	支持外部任务. 
  <LI>bt.multiscrape 打开/关闭多次查询状态功能. 无需手动更改选项, µTorrent 会自动检测 tracker 服务器是否支持这一功能. 
  <LI>bt.send_have_to_seed 打开/关闭发送已有消息给种子. 
  <LI>bt.set_sockbuf 调试选项, 用来自动检测 TCP 缓冲大小(so_sndbuf) 并根据上传速度自动调整. 
  <LI>bt.connect_speed 控制 µTorrent 每秒的连接数
  <LI>bt.prio_first_last_piece 禁用或启用调整每个文件的第一块和最后一块的优先级. 
  <LI>bt.allow_same_ip 允许来自同一 IP 的多个连接. 一般来说无需启用, 可用来阻止那些只下载不上传的用户. 
  <LI>bt.no_connect_to_services 不连接使用 25 或 110 端口的用户. 如果你想使用反病毒软件的邮件检测则启用此选项. 
  <LI>peer.lazy_bitfield 帮助对抗某些 ISP 干扰做种. 这一选项不会始终工作. 
  <LI>peer.resolve_country µTorrent 使用 <A href="http://en.wikipedia.org/wiki/DNSBL">DNSBL</A> 服务来解析 IP 所属国家. 不能和 flags.conf 文件同时使用! 
  <LI>peer.disconnect_inactive 启用或禁用在一段时间后(默认为 5 分钟)断开不活动用户的功能. 但是如果群集中用户数低于设置的任务最大连接数则不会断开. 
  <LI>peer.disconnect_inactive_interval 设置用户被视作不活动状态的时间. µTorrent 自动忽视低于 300 的值. 
  <LI>diskio.flush_files 设置 µTorrent 按分钟关闭文件句柄. 这可以避免发生内存泄露的情况.
  <LI>diskio.sparse_files 仅可用于使用 NTFS(2k/XP/2003) 格式的分区. 打开此功能后, 文件仅会在写入时才会分配空间. 当然这会增加磁盘碎片, 但是可以减少磁盘占用. 不能与 pre-allocate 同时使用. 
  <LI>diskio.use_partfile 用来保存那些你标记为取消下载的文件的数据. 在避免为文件分配空间时非常有用. 尽管我们不需要下载某些文件, 但是 µTorrent 依然会下载少量分块信息并保存为 dat 文件. 不要与 compact allocation 功能同时使用. 
  <LI>diskio.smart_hash 启用 µTorrent 在内存(写入缓冲队列)中校验数据的功能, 避免直接在磁盘上校验或从磁盘上重新读取. 这对降低磁盘读取非常有用, 特别是在拥有一个很高的下载速度时. 
  <LI>diskio.coalesce_writes 尝试使用最少次数的调用写入文件功能, 有助于降低磁盘写入次数 (不可避免的会稍微提高内存和 CPU 的使用率). </LI></UL>
<H3><A name=What_is_ipfilter.dat.3F></A>文件 ipfilter.dat 有什么作用?</H3>
<P>这是一个用来指定被阻止的 IP 范围的文本文件. 详细格式为: xxx.xxx.xxx.xxx - yyy.yyy.yyy.yyy <BR>你也可以设置单个 IP 地址(如: xxx.xxx.xxx.xxx). 
注意: 每行仅可输入一条规则. </P>
<P>不要在 IP 地址前加多余的 "0" (如: 不要将 64.12.15.0 写为 064.012.015.000), 否则阻止规则将无效. </P>
<P>将该文件置于文件夹 %AppData%\uTorrent 中, 然后将高级选项中的参数 ipfilter.enable 设置为 "启用".</P>
<P>需要重新载入文件 ipfilter.dat 而又不想重新运行 µTorrent, 请先将 ipfilter.enable 设置为 "禁用", 然后重新设置为 "启用" 即可. 
但是这样做的话规则将不会应用于已有连接, 而仅会作用于新的连接. 如果想将阻止规则应用于已有任务则必须重新运行 µTorrent.</P>
<H3><A name=What_is_flags.conf.3F></A>文件 flags.conf 有什么作用?</H3>
<P>flags.conf 是一个用来指示主机位于哪个国家的文本文件. 你可以指定 .net 和 .com 域名到某个国家. 详细格式为 域名|国家代码. 点击这里下载由 eng 创建的 <A 
href="http://forum.utorrent.com/viewtopic.php?id=2070">flags.conf</A> 文件. 按下 Ctrl+Shift+R 将使 µTorrent 载入文件并生效. 请将它置于文件夹 %AppData%\uTorrent 中. </P>
<P>命名为 00 的标识用来表示那些没有指明的 .com/.net 域名并显示为美国国旗.</P>
<P>注意, 这个功能不能与 peer.resolve_country 同时启用. </P>
<H3><A name=Who_makes_.C2.B5Torrent.3F></A>µTorrent 的作者是谁?</H3>
<P>Ludvig Strigeus (ludde) 编写并维护 µTorrent 的原始版本. </P>
<P>BitTorrent Inc 的程序员是当前版本 µTorrent 的开发人员. </P>
<P>Giancarlo Martínez (Firon) 一直在维护 µTorrent 论坛和帮助. </P>
<P>Timothy Su (ignorantcow) 是 µTorrent 官方网站的设计人员. </P>
<H3><A name=How_can_.C2.B5Torrent_be_so_small_and_so_fast.3F></A>为什么 µTorrent 体积如此之小且运行速度飞快?</H3>
<P>µTorrent 使用自行开发的 C++ 代码库编写, 然后使用 EXE 文件压缩程序再次压缩, 所以体积很小且运行速度飞快. </P>
<H3><A name=How_do_you_write_.C2.B5.3F></A>如何输入字符 µ?</H3>
<P>通用方法: 切换到标准英文输入法, 然后按住 Alt 键不放, 依次按下小键盘上的 "0", "1", "8", "1" 四个按键即可输入. </P>
<H3><A name=How.2Fwhere_can_I_get_the_latest_.C2.B5Torrent_beta.3F></A>从哪里可以获得最新版本的 µTorrent?</H3>
<P>如果你这样问, 说明你就不可能找到它. :) </P>
<P>一般来说, 公开测试版一般发布于<a target="_blank" href="http://www.utorrent.com/download.php">下载页面</a>上.</P><BR>
<SCRIPT type=text/javascript>loadToc();</SCRIPT>
</DIV></DIV>
<DIV id=footer>
<P><span style="font-weight: 400"><font face="Tahoma" size="2">
<a href="http://www.bittorrent.com/">版权属于 风之痕 &amp; lemon</a></font></span></P></DIV></DIV></DIV></DIV>
