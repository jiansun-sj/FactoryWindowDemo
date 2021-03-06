﻿// ==================================================
// 文件名：MainWindowViewModel.cs
// 创建时间：2020/04/12 16:25
// 上海芸浦信息技术有限公司
// copyright@yumpoo
// ==================================================
// 最后修改于：2020/05/11 16:25
// 修改人：jians
// ==================================================

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FactoryWindowGUI.Annotations;
using FactoryWindowGUI.Model;
using FactoryWindowGUI.Util;
using log4net;
using ProcessControlService.WCFClients;

namespace FactoryWindowGUI.ViewModel
{
    /// <summary>
    ///     create by Charlotte
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MainWindowViewModel));

        private ObservableCollection<ConnectionNameModel> _connectionList;

        private string _connectionStatus;

        private string _redundancyStatus = "未知";

        private ConnectionNameModel _selectedConnectionName;

        public AdminUtil AdminUtil = new AdminUtil();

        /*public MachineUtil MachineUtil = new MachineUtil();

        public ProcessUtil ProcessUtil = new ProcessUtil();

        public ResourceUtil ResourceUtil = new ResourceUtil();*/

        public string Title { get; set; } = "FactoryWindow";

        public string RedundancyStatus
        {
            get => _redundancyStatus;
            set
            {
                _redundancyStatus = value;
                OnPropertyChanged(nameof(RedundancyStatus));
            }
        }

        public string ConnectionStatus
        {
            get
            {
                _connectionStatus =
                    /*MachineUtil.Connected && ProcessUtil.Connected && */
                    AdminUtil.Connected /*&& ResourceUtil.Connected*/
                        ? "已连接"
                        : "未连接";
                return _connectionStatus;
            }
            set
            {
                if (_connectionStatus == value) return;
                _connectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus));
            }
        }

        public ObservableCollection<ConnectionNameModel> ConnectionList
        {
            get
            {
                var connectionList = HostConnectionManager.GetAllGroupNames();

                _connectionList = new ObservableCollection<ConnectionNameModel>();
                if (connectionList == null) return _connectionList;
                for (var i = 0; i < connectionList.Count; i++)
                    _connectionList.Add(new ConnectionNameModel {Id = i + 1, ConnectionName = connectionList[i]});
                return _connectionList;
            }
            set
            {
                _connectionList = value;
                OnPropertyChanged(nameof(ConnectionList));
            }
        }

        public ConnectionNameModel SelectedConnectionName
        {
            get
            {
                var hostCurrentConn = HostConnectionManager.CurrentGroup();

                foreach (var conn in _connectionList)
                    if (conn.ConnectionName == hostCurrentConn)
                        _selectedConnectionName = conn;
                return _selectedConnectionName;
            }
            set
            {
                if (_selectedConnectionName == value) return;
                _selectedConnectionName = value;

                HostConnectionManager.SelectCurrentGroup(_selectedConnectionName.ConnectionName);

                /*MachineUtil.ConnectToServer();
                ProcessUtil.ConnectToServer();*/
                AdminUtil.ConnectToServer();
                /*ResourceUtil.ConnectToServer();*/

                OnPropertyChanged(nameof(ConnectionStatus));
                OnPropertyChanged(nameof(ConnectionStatus));
                OnPropertyChanged(nameof(SelectedConnectionName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}