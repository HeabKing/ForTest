﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace _2016_03_22LinqStudy
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="DataSource")]
	public partial class dbDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertTblAnswer(TblAnswer instance);
    partial void UpdateTblAnswer(TblAnswer instance);
    partial void DeleteTblAnswer(TblAnswer instance);
    partial void InsertTblSurvey(TblSurvey instance);
    partial void UpdateTblSurvey(TblSurvey instance);
    partial void DeleteTblSurvey(TblSurvey instance);
    partial void InsertTblOption(TblOption instance);
    partial void UpdateTblOption(TblOption instance);
    partial void DeleteTblOption(TblOption instance);
    partial void InsertTblQuestion(TblQuestion instance);
    partial void UpdateTblQuestion(TblQuestion instance);
    partial void DeleteTblQuestion(TblQuestion instance);
    #endregion
		
		public dbDataContext() : 
				base(global::_2016_03_22LinqStudy.Properties.Settings.Default.DataSourceConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public dbDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dbDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dbDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dbDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TblAnswer> TblAnswer
		{
			get
			{
				return this.GetTable<TblAnswer>();
			}
		}
		
		public System.Data.Linq.Table<TblSurvey> TblSurvey
		{
			get
			{
				return this.GetTable<TblSurvey>();
			}
		}
		
		public System.Data.Linq.Table<TblOption> TblOption
		{
			get
			{
				return this.GetTable<TblOption>();
			}
		}
		
		public System.Data.Linq.Table<TblQuestion> TblQuestion
		{
			get
			{
				return this.GetTable<TblQuestion>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TblAnswer")]
	public partial class TblAnswer : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _OptionId;
		
		private System.Guid _GroupId;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnOptionIdChanging(int value);
    partial void OnOptionIdChanged();
    partial void OnGroupIdChanging(System.Guid value);
    partial void OnGroupIdChanged();
    #endregion
		
		public TblAnswer()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OptionId", DbType="Int NOT NULL")]
		public int OptionId
		{
			get
			{
				return this._OptionId;
			}
			set
			{
				if ((this._OptionId != value))
				{
					this.OnOptionIdChanging(value);
					this.SendPropertyChanging();
					this._OptionId = value;
					this.SendPropertyChanged("OptionId");
					this.OnOptionIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid GroupId
		{
			get
			{
				return this._GroupId;
			}
			set
			{
				if ((this._GroupId != value))
				{
					this.OnGroupIdChanging(value);
					this.SendPropertyChanging();
					this._GroupId = value;
					this.SendPropertyChanged("GroupId");
					this.OnGroupIdChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TblSurvey")]
	public partial class TblSurvey : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Title;
		
		private string _Desc;
		
		private System.Guid _UserId;
		
		private bool _IsDelete;
		
		private System.DateTime _CreateTime;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnDescChanging(string value);
    partial void OnDescChanged();
    partial void OnUserIdChanging(System.Guid value);
    partial void OnUserIdChanged();
    partial void OnIsDeleteChanging(bool value);
    partial void OnIsDeleteChanged();
    partial void OnCreateTimeChanging(System.DateTime value);
    partial void OnCreateTimeChanged();
    #endregion
		
		public TblSurvey()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Desc]", Storage="_Desc", DbType="NVarChar(1024) NOT NULL", CanBeNull=false)]
		public string Desc
		{
			get
			{
				return this._Desc;
			}
			set
			{
				if ((this._Desc != value))
				{
					this.OnDescChanging(value);
					this.SendPropertyChanging();
					this._Desc = value;
					this.SendPropertyChanged("Desc");
					this.OnDescChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDelete", DbType="Bit NOT NULL")]
		public bool IsDelete
		{
			get
			{
				return this._IsDelete;
			}
			set
			{
				if ((this._IsDelete != value))
				{
					this.OnIsDeleteChanging(value);
					this.SendPropertyChanging();
					this._IsDelete = value;
					this.SendPropertyChanged("IsDelete");
					this.OnIsDeleteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateTime", DbType="DateTime NOT NULL")]
		public System.DateTime CreateTime
		{
			get
			{
				return this._CreateTime;
			}
			set
			{
				if ((this._CreateTime != value))
				{
					this.OnCreateTimeChanging(value);
					this.SendPropertyChanging();
					this._CreateTime = value;
					this.SendPropertyChanged("CreateTime");
					this.OnCreateTimeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TblOption")]
	public partial class TblOption : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Desc;
		
		private int _QuesId;
		
		private bool _IsDelete;
		
		private System.DateTime _CreateTime;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnDescChanging(string value);
    partial void OnDescChanged();
    partial void OnQuesIdChanging(int value);
    partial void OnQuesIdChanged();
    partial void OnIsDeleteChanging(bool value);
    partial void OnIsDeleteChanged();
    partial void OnCreateTimeChanging(System.DateTime value);
    partial void OnCreateTimeChanged();
    #endregion
		
		public TblOption()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Desc]", Storage="_Desc", DbType="NVarChar(250) NOT NULL", CanBeNull=false)]
		public string Desc
		{
			get
			{
				return this._Desc;
			}
			set
			{
				if ((this._Desc != value))
				{
					this.OnDescChanging(value);
					this.SendPropertyChanging();
					this._Desc = value;
					this.SendPropertyChanged("Desc");
					this.OnDescChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QuesId", DbType="Int NOT NULL")]
		public int QuesId
		{
			get
			{
				return this._QuesId;
			}
			set
			{
				if ((this._QuesId != value))
				{
					this.OnQuesIdChanging(value);
					this.SendPropertyChanging();
					this._QuesId = value;
					this.SendPropertyChanged("QuesId");
					this.OnQuesIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDelete", DbType="Bit NOT NULL")]
		public bool IsDelete
		{
			get
			{
				return this._IsDelete;
			}
			set
			{
				if ((this._IsDelete != value))
				{
					this.OnIsDeleteChanging(value);
					this.SendPropertyChanging();
					this._IsDelete = value;
					this.SendPropertyChanged("IsDelete");
					this.OnIsDeleteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateTime", DbType="DateTime NOT NULL")]
		public System.DateTime CreateTime
		{
			get
			{
				return this._CreateTime;
			}
			set
			{
				if ((this._CreateTime != value))
				{
					this.OnCreateTimeChanging(value);
					this.SendPropertyChanging();
					this._CreateTime = value;
					this.SendPropertyChanged("CreateTime");
					this.OnCreateTimeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TblQuestion")]
	public partial class TblQuestion : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Desc;
		
		private bool _IsSingleOption;
		
		private int _SurveyId;
		
		private bool _IsDelete;
		
		private System.DateTime _CreateTime;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnDescChanging(string value);
    partial void OnDescChanged();
    partial void OnIsSingleOptionChanging(bool value);
    partial void OnIsSingleOptionChanged();
    partial void OnSurveyIdChanging(int value);
    partial void OnSurveyIdChanged();
    partial void OnIsDeleteChanging(bool value);
    partial void OnIsDeleteChanged();
    partial void OnCreateTimeChanging(System.DateTime value);
    partial void OnCreateTimeChanged();
    #endregion
		
		public TblQuestion()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Desc]", Storage="_Desc", DbType="NVarChar(250) NOT NULL", CanBeNull=false)]
		public string Desc
		{
			get
			{
				return this._Desc;
			}
			set
			{
				if ((this._Desc != value))
				{
					this.OnDescChanging(value);
					this.SendPropertyChanging();
					this._Desc = value;
					this.SendPropertyChanged("Desc");
					this.OnDescChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsSingleOption", DbType="Bit NOT NULL")]
		public bool IsSingleOption
		{
			get
			{
				return this._IsSingleOption;
			}
			set
			{
				if ((this._IsSingleOption != value))
				{
					this.OnIsSingleOptionChanging(value);
					this.SendPropertyChanging();
					this._IsSingleOption = value;
					this.SendPropertyChanged("IsSingleOption");
					this.OnIsSingleOptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SurveyId", DbType="Int NOT NULL")]
		public int SurveyId
		{
			get
			{
				return this._SurveyId;
			}
			set
			{
				if ((this._SurveyId != value))
				{
					this.OnSurveyIdChanging(value);
					this.SendPropertyChanging();
					this._SurveyId = value;
					this.SendPropertyChanged("SurveyId");
					this.OnSurveyIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDelete", DbType="Bit NOT NULL")]
		public bool IsDelete
		{
			get
			{
				return this._IsDelete;
			}
			set
			{
				if ((this._IsDelete != value))
				{
					this.OnIsDeleteChanging(value);
					this.SendPropertyChanging();
					this._IsDelete = value;
					this.SendPropertyChanged("IsDelete");
					this.OnIsDeleteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateTime", DbType="DateTime NOT NULL")]
		public System.DateTime CreateTime
		{
			get
			{
				return this._CreateTime;
			}
			set
			{
				if ((this._CreateTime != value))
				{
					this.OnCreateTimeChanging(value);
					this.SendPropertyChanging();
					this._CreateTime = value;
					this.SendPropertyChanged("CreateTime");
					this.OnCreateTimeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
