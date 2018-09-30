
/*
'===============================================================================
'  Generated From - CSharp_dOOdads_BusinessEntity.vbgen
' 
'  ** IMPORTANT  ** 
'  How to Generate your stored procedures:
' 
'  SQL        = SQL_StoredProcs.vbgen
'  ACCESS     = Access_StoredProcs.vbgen
'  ORACLE     = Oracle_StoredProcs.vbgen
'  FIREBIRD   = FirebirdStoredProcs.vbgen
'  POSTGRESQL = PostgreSQL_StoredProcs.vbgen
'
'  The supporting base class SqlClientEntity is in the Architecture directory in "dOOdads".
'  
'  This object is 'abstract' which means you need to inherit from it to be able
'  to instantiate it.  This is very easilly done. You can override properties and
'  methods in your derived class, this allows you to regenerate this class at any
'  time and not worry about overwriting custom code. 
'
'  NEVER EDIT THIS FILE.
'
'  public class YourObject :  _YourObject
'  {
'
'  }
'
'===============================================================================
*/

// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;

using MyGeneration.dOOdads;

namespace BusinessLayer
{
	public abstract class _tblMenuItem : SqlClientEntity
	{
		public _tblMenuItem()
		{
			this.QuerySource = "tblMenuItem";
			this.MappingName = "tblMenuItem";

		}	

		//=================================================================
		//  public Overrides void AddNew()
		//=================================================================
		//
		//=================================================================
		public override void AddNew()
		{
			base.AddNew();
			
		}
		
		
		public override void FlushData()
		{
			this._whereClause = null;
			this._aggregateClause = null;
			base.FlushData();
		}
		
		//=================================================================
		//  	public Function LoadAll() As Boolean
		//=================================================================
		//  Loads all of the records in the database, and sets the currentRow to the first row
		//=================================================================
		public bool LoadAll() 
		{
			ListDictionary parameters = null;
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblMenuItemLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int AppMenuItemId)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.AppMenuItemId, AppMenuItemId);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblMenuItemLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter AppMenuItemId
			{
				get
				{
					return new SqlParameter("@AppMenuItemId", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppMenuTypeId
			{
				get
				{
					return new SqlParameter("@AppMenuTypeId", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppMenuItem
			{
				get
				{
					return new SqlParameter("@AppMenuItem", SqlDbType.NVarChar, 100);
				}
			}
			
			public static SqlParameter AppParentId
			{
				get
				{
					return new SqlParameter("@AppParentId", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppDisplayOrder
			{
				get
				{
					return new SqlParameter("@AppDisplayOrder", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppPageId
			{
				get
				{
					return new SqlParameter("@AppPageId", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppIsActive
			{
				get
				{
					return new SqlParameter("@AppIsActive", SqlDbType.Bit, 0);
				}
			}
			
			public static SqlParameter AppCreatedDate
			{
				get
				{
					return new SqlParameter("@AppCreatedDate", SqlDbType.DateTime, 0);
				}
			}
			
			public static SqlParameter AppCreatedBy
			{
				get
				{
					return new SqlParameter("@AppCreatedBy", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppMenuItemTypeID
			{
				get
				{
					return new SqlParameter("@AppMenuItemTypeID", SqlDbType.Int, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string AppMenuItemId = "appMenuItemId";
            public const string AppMenuTypeId = "appMenuTypeId";
            public const string AppMenuItem = "appMenuItem";
            public const string AppParentId = "appParentId";
            public const string AppDisplayOrder = "appDisplayOrder";
            public const string AppPageId = "appPageId";
            public const string AppIsActive = "appIsActive";
            public const string AppCreatedDate = "appCreatedDate";
            public const string AppCreatedBy = "appCreatedBy";
            public const string AppMenuItemTypeID = "appMenuItemTypeID";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppMenuItemId] = _tblMenuItem.PropertyNames.AppMenuItemId;
					ht[AppMenuTypeId] = _tblMenuItem.PropertyNames.AppMenuTypeId;
					ht[AppMenuItem] = _tblMenuItem.PropertyNames.AppMenuItem;
					ht[AppParentId] = _tblMenuItem.PropertyNames.AppParentId;
					ht[AppDisplayOrder] = _tblMenuItem.PropertyNames.AppDisplayOrder;
					ht[AppPageId] = _tblMenuItem.PropertyNames.AppPageId;
					ht[AppIsActive] = _tblMenuItem.PropertyNames.AppIsActive;
					ht[AppCreatedDate] = _tblMenuItem.PropertyNames.AppCreatedDate;
					ht[AppCreatedBy] = _tblMenuItem.PropertyNames.AppCreatedBy;
					ht[AppMenuItemTypeID] = _tblMenuItem.PropertyNames.AppMenuItemTypeID;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string AppMenuItemId = "AppMenuItemId";
            public const string AppMenuTypeId = "AppMenuTypeId";
            public const string AppMenuItem = "AppMenuItem";
            public const string AppParentId = "AppParentId";
            public const string AppDisplayOrder = "AppDisplayOrder";
            public const string AppPageId = "AppPageId";
            public const string AppIsActive = "AppIsActive";
            public const string AppCreatedDate = "AppCreatedDate";
            public const string AppCreatedBy = "AppCreatedBy";
            public const string AppMenuItemTypeID = "AppMenuItemTypeID";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppMenuItemId] = _tblMenuItem.ColumnNames.AppMenuItemId;
					ht[AppMenuTypeId] = _tblMenuItem.ColumnNames.AppMenuTypeId;
					ht[AppMenuItem] = _tblMenuItem.ColumnNames.AppMenuItem;
					ht[AppParentId] = _tblMenuItem.ColumnNames.AppParentId;
					ht[AppDisplayOrder] = _tblMenuItem.ColumnNames.AppDisplayOrder;
					ht[AppPageId] = _tblMenuItem.ColumnNames.AppPageId;
					ht[AppIsActive] = _tblMenuItem.ColumnNames.AppIsActive;
					ht[AppCreatedDate] = _tblMenuItem.ColumnNames.AppCreatedDate;
					ht[AppCreatedBy] = _tblMenuItem.ColumnNames.AppCreatedBy;
					ht[AppMenuItemTypeID] = _tblMenuItem.ColumnNames.AppMenuItemTypeID;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string AppMenuItemId = "s_AppMenuItemId";
            public const string AppMenuTypeId = "s_AppMenuTypeId";
            public const string AppMenuItem = "s_AppMenuItem";
            public const string AppParentId = "s_AppParentId";
            public const string AppDisplayOrder = "s_AppDisplayOrder";
            public const string AppPageId = "s_AppPageId";
            public const string AppIsActive = "s_AppIsActive";
            public const string AppCreatedDate = "s_AppCreatedDate";
            public const string AppCreatedBy = "s_AppCreatedBy";
            public const string AppMenuItemTypeID = "s_AppMenuItemTypeID";

		}
		#endregion		
		
		#region Properties
	
		public virtual int AppMenuItemId
	    {
			get
	        {
				return base.Getint(ColumnNames.AppMenuItemId);
			}
			set
	        {
				base.Setint(ColumnNames.AppMenuItemId, value);
			}
		}

		public virtual int AppMenuTypeId
	    {
			get
	        {
				return base.Getint(ColumnNames.AppMenuTypeId);
			}
			set
	        {
				base.Setint(ColumnNames.AppMenuTypeId, value);
			}
		}

		public virtual string AppMenuItem
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppMenuItem);
			}
			set
	        {
				base.Setstring(ColumnNames.AppMenuItem, value);
			}
		}

		public virtual int AppParentId
	    {
			get
	        {
				return base.Getint(ColumnNames.AppParentId);
			}
			set
	        {
				base.Setint(ColumnNames.AppParentId, value);
			}
		}

		public virtual int AppDisplayOrder
	    {
			get
	        {
				return base.Getint(ColumnNames.AppDisplayOrder);
			}
			set
	        {
				base.Setint(ColumnNames.AppDisplayOrder, value);
			}
		}

		public virtual int AppPageId
	    {
			get
	        {
				return base.Getint(ColumnNames.AppPageId);
			}
			set
	        {
				base.Setint(ColumnNames.AppPageId, value);
			}
		}

		public virtual bool AppIsActive
	    {
			get
	        {
				return base.Getbool(ColumnNames.AppIsActive);
			}
			set
	        {
				base.Setbool(ColumnNames.AppIsActive, value);
			}
		}

		public virtual DateTime AppCreatedDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.AppCreatedDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.AppCreatedDate, value);
			}
		}

		public virtual int AppCreatedBy
	    {
			get
	        {
				return base.Getint(ColumnNames.AppCreatedBy);
			}
			set
	        {
				base.Setint(ColumnNames.AppCreatedBy, value);
			}
		}

		public virtual int AppMenuItemTypeID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppMenuItemTypeID);
			}
			set
	        {
				base.Setint(ColumnNames.AppMenuItemTypeID, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_AppMenuItemId
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppMenuItemId) ? string.Empty : base.GetintAsString(ColumnNames.AppMenuItemId);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppMenuItemId);
				else
					this.AppMenuItemId = base.SetintAsString(ColumnNames.AppMenuItemId, value);
			}
		}

		public virtual string s_AppMenuTypeId
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppMenuTypeId) ? string.Empty : base.GetintAsString(ColumnNames.AppMenuTypeId);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppMenuTypeId);
				else
					this.AppMenuTypeId = base.SetintAsString(ColumnNames.AppMenuTypeId, value);
			}
		}

		public virtual string s_AppMenuItem
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppMenuItem) ? string.Empty : base.GetstringAsString(ColumnNames.AppMenuItem);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppMenuItem);
				else
					this.AppMenuItem = base.SetstringAsString(ColumnNames.AppMenuItem, value);
			}
		}

		public virtual string s_AppParentId
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppParentId) ? string.Empty : base.GetintAsString(ColumnNames.AppParentId);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppParentId);
				else
					this.AppParentId = base.SetintAsString(ColumnNames.AppParentId, value);
			}
		}

		public virtual string s_AppDisplayOrder
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppDisplayOrder) ? string.Empty : base.GetintAsString(ColumnNames.AppDisplayOrder);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppDisplayOrder);
				else
					this.AppDisplayOrder = base.SetintAsString(ColumnNames.AppDisplayOrder, value);
			}
		}

		public virtual string s_AppPageId
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppPageId) ? string.Empty : base.GetintAsString(ColumnNames.AppPageId);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppPageId);
				else
					this.AppPageId = base.SetintAsString(ColumnNames.AppPageId, value);
			}
		}

		public virtual string s_AppIsActive
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppIsActive) ? string.Empty : base.GetboolAsString(ColumnNames.AppIsActive);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppIsActive);
				else
					this.AppIsActive = base.SetboolAsString(ColumnNames.AppIsActive, value);
			}
		}

		public virtual string s_AppCreatedDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppCreatedDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.AppCreatedDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppCreatedDate);
				else
					this.AppCreatedDate = base.SetDateTimeAsString(ColumnNames.AppCreatedDate, value);
			}
		}

		public virtual string s_AppCreatedBy
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppCreatedBy) ? string.Empty : base.GetintAsString(ColumnNames.AppCreatedBy);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppCreatedBy);
				else
					this.AppCreatedBy = base.SetintAsString(ColumnNames.AppCreatedBy, value);
			}
		}

		public virtual string s_AppMenuItemTypeID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppMenuItemTypeID) ? string.Empty : base.GetintAsString(ColumnNames.AppMenuItemTypeID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppMenuItemTypeID);
				else
					this.AppMenuItemTypeID = base.SetintAsString(ColumnNames.AppMenuItemTypeID, value);
			}
		}


		#endregion		
	
		#region Where Clause
		public class WhereClause
		{
			public WhereClause(BusinessEntity entity)
			{
				this._entity = entity;
			}
			
			public TearOffWhereParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffWhereParameter(this);
					}

					return _tearOff;
				}
			}

			#region WhereParameter TearOff's
			public class TearOffWhereParameter
			{
				public TearOffWhereParameter(WhereClause clause)
				{
					this._clause = clause;
				}
				
				
				public WhereParameter AppMenuItemId
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppMenuItemId, Parameters.AppMenuItemId);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppMenuTypeId
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppMenuTypeId, Parameters.AppMenuTypeId);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppMenuItem
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppMenuItem, Parameters.AppMenuItem);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppParentId
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppParentId, Parameters.AppParentId);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppDisplayOrder
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppDisplayOrder, Parameters.AppDisplayOrder);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppPageId
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppPageId, Parameters.AppPageId);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppIsActive
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppIsActive, Parameters.AppIsActive);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppCreatedDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppCreatedDate, Parameters.AppCreatedDate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppCreatedBy
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppCreatedBy, Parameters.AppCreatedBy);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppMenuItemTypeID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppMenuItemTypeID, Parameters.AppMenuItemTypeID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter AppMenuItemId
		    {
				get
		        {
					if(_AppMenuItemId_W == null)
	        	    {
						_AppMenuItemId_W = TearOff.AppMenuItemId;
					}
					return _AppMenuItemId_W;
				}
			}

			public WhereParameter AppMenuTypeId
		    {
				get
		        {
					if(_AppMenuTypeId_W == null)
	        	    {
						_AppMenuTypeId_W = TearOff.AppMenuTypeId;
					}
					return _AppMenuTypeId_W;
				}
			}

			public WhereParameter AppMenuItem
		    {
				get
		        {
					if(_AppMenuItem_W == null)
	        	    {
						_AppMenuItem_W = TearOff.AppMenuItem;
					}
					return _AppMenuItem_W;
				}
			}

			public WhereParameter AppParentId
		    {
				get
		        {
					if(_AppParentId_W == null)
	        	    {
						_AppParentId_W = TearOff.AppParentId;
					}
					return _AppParentId_W;
				}
			}

			public WhereParameter AppDisplayOrder
		    {
				get
		        {
					if(_AppDisplayOrder_W == null)
	        	    {
						_AppDisplayOrder_W = TearOff.AppDisplayOrder;
					}
					return _AppDisplayOrder_W;
				}
			}

			public WhereParameter AppPageId
		    {
				get
		        {
					if(_AppPageId_W == null)
	        	    {
						_AppPageId_W = TearOff.AppPageId;
					}
					return _AppPageId_W;
				}
			}

			public WhereParameter AppIsActive
		    {
				get
		        {
					if(_AppIsActive_W == null)
	        	    {
						_AppIsActive_W = TearOff.AppIsActive;
					}
					return _AppIsActive_W;
				}
			}

			public WhereParameter AppCreatedDate
		    {
				get
		        {
					if(_AppCreatedDate_W == null)
	        	    {
						_AppCreatedDate_W = TearOff.AppCreatedDate;
					}
					return _AppCreatedDate_W;
				}
			}

			public WhereParameter AppCreatedBy
		    {
				get
		        {
					if(_AppCreatedBy_W == null)
	        	    {
						_AppCreatedBy_W = TearOff.AppCreatedBy;
					}
					return _AppCreatedBy_W;
				}
			}

			public WhereParameter AppMenuItemTypeID
		    {
				get
		        {
					if(_AppMenuItemTypeID_W == null)
	        	    {
						_AppMenuItemTypeID_W = TearOff.AppMenuItemTypeID;
					}
					return _AppMenuItemTypeID_W;
				}
			}

			private WhereParameter _AppMenuItemId_W = null;
			private WhereParameter _AppMenuTypeId_W = null;
			private WhereParameter _AppMenuItem_W = null;
			private WhereParameter _AppParentId_W = null;
			private WhereParameter _AppDisplayOrder_W = null;
			private WhereParameter _AppPageId_W = null;
			private WhereParameter _AppIsActive_W = null;
			private WhereParameter _AppCreatedDate_W = null;
			private WhereParameter _AppCreatedBy_W = null;
			private WhereParameter _AppMenuItemTypeID_W = null;

			public void WhereClauseReset()
			{
				_AppMenuItemId_W = null;
				_AppMenuTypeId_W = null;
				_AppMenuItem_W = null;
				_AppParentId_W = null;
				_AppDisplayOrder_W = null;
				_AppPageId_W = null;
				_AppIsActive_W = null;
				_AppCreatedDate_W = null;
				_AppCreatedBy_W = null;
				_AppMenuItemTypeID_W = null;

				this._entity.Query.FlushWhereParameters();

			}
	
			private BusinessEntity _entity;
			private TearOffWhereParameter _tearOff;
			
		}
	
		public WhereClause Where
		{
			get
			{
				if(_whereClause == null)
				{
					_whereClause = new WhereClause(this);
				}
		
				return _whereClause;
			}
		}
		
		private WhereClause _whereClause = null;	
		#endregion
	
		#region Aggregate Clause
		public class AggregateClause
		{
			public AggregateClause(BusinessEntity entity)
			{
				this._entity = entity;
			}
			
			public TearOffAggregateParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffAggregateParameter(this);
					}

					return _tearOff;
				}
			}

			#region AggregateParameter TearOff's
			public class TearOffAggregateParameter
			{
				public TearOffAggregateParameter(AggregateClause clause)
				{
					this._clause = clause;
				}
				
				
				public AggregateParameter AppMenuItemId
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppMenuItemId, Parameters.AppMenuItemId);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppMenuTypeId
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppMenuTypeId, Parameters.AppMenuTypeId);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppMenuItem
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppMenuItem, Parameters.AppMenuItem);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppParentId
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppParentId, Parameters.AppParentId);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppDisplayOrder
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppDisplayOrder, Parameters.AppDisplayOrder);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppPageId
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppPageId, Parameters.AppPageId);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppIsActive
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppIsActive, Parameters.AppIsActive);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppCreatedDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppCreatedDate, Parameters.AppCreatedDate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppCreatedBy
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppCreatedBy, Parameters.AppCreatedBy);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppMenuItemTypeID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppMenuItemTypeID, Parameters.AppMenuItemTypeID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter AppMenuItemId
		    {
				get
		        {
					if(_AppMenuItemId_W == null)
	        	    {
						_AppMenuItemId_W = TearOff.AppMenuItemId;
					}
					return _AppMenuItemId_W;
				}
			}

			public AggregateParameter AppMenuTypeId
		    {
				get
		        {
					if(_AppMenuTypeId_W == null)
	        	    {
						_AppMenuTypeId_W = TearOff.AppMenuTypeId;
					}
					return _AppMenuTypeId_W;
				}
			}

			public AggregateParameter AppMenuItem
		    {
				get
		        {
					if(_AppMenuItem_W == null)
	        	    {
						_AppMenuItem_W = TearOff.AppMenuItem;
					}
					return _AppMenuItem_W;
				}
			}

			public AggregateParameter AppParentId
		    {
				get
		        {
					if(_AppParentId_W == null)
	        	    {
						_AppParentId_W = TearOff.AppParentId;
					}
					return _AppParentId_W;
				}
			}

			public AggregateParameter AppDisplayOrder
		    {
				get
		        {
					if(_AppDisplayOrder_W == null)
	        	    {
						_AppDisplayOrder_W = TearOff.AppDisplayOrder;
					}
					return _AppDisplayOrder_W;
				}
			}

			public AggregateParameter AppPageId
		    {
				get
		        {
					if(_AppPageId_W == null)
	        	    {
						_AppPageId_W = TearOff.AppPageId;
					}
					return _AppPageId_W;
				}
			}

			public AggregateParameter AppIsActive
		    {
				get
		        {
					if(_AppIsActive_W == null)
	        	    {
						_AppIsActive_W = TearOff.AppIsActive;
					}
					return _AppIsActive_W;
				}
			}

			public AggregateParameter AppCreatedDate
		    {
				get
		        {
					if(_AppCreatedDate_W == null)
	        	    {
						_AppCreatedDate_W = TearOff.AppCreatedDate;
					}
					return _AppCreatedDate_W;
				}
			}

			public AggregateParameter AppCreatedBy
		    {
				get
		        {
					if(_AppCreatedBy_W == null)
	        	    {
						_AppCreatedBy_W = TearOff.AppCreatedBy;
					}
					return _AppCreatedBy_W;
				}
			}

			public AggregateParameter AppMenuItemTypeID
		    {
				get
		        {
					if(_AppMenuItemTypeID_W == null)
	        	    {
						_AppMenuItemTypeID_W = TearOff.AppMenuItemTypeID;
					}
					return _AppMenuItemTypeID_W;
				}
			}

			private AggregateParameter _AppMenuItemId_W = null;
			private AggregateParameter _AppMenuTypeId_W = null;
			private AggregateParameter _AppMenuItem_W = null;
			private AggregateParameter _AppParentId_W = null;
			private AggregateParameter _AppDisplayOrder_W = null;
			private AggregateParameter _AppPageId_W = null;
			private AggregateParameter _AppIsActive_W = null;
			private AggregateParameter _AppCreatedDate_W = null;
			private AggregateParameter _AppCreatedBy_W = null;
			private AggregateParameter _AppMenuItemTypeID_W = null;

			public void AggregateClauseReset()
			{
				_AppMenuItemId_W = null;
				_AppMenuTypeId_W = null;
				_AppMenuItem_W = null;
				_AppParentId_W = null;
				_AppDisplayOrder_W = null;
				_AppPageId_W = null;
				_AppIsActive_W = null;
				_AppCreatedDate_W = null;
				_AppCreatedBy_W = null;
				_AppMenuItemTypeID_W = null;

				this._entity.Query.FlushAggregateParameters();

			}
	
			private BusinessEntity _entity;
			private TearOffAggregateParameter _tearOff;
			
		}
	
		public AggregateClause Aggregate
		{
			get
			{
				if(_aggregateClause == null)
				{
					_aggregateClause = new AggregateClause(this);
				}
		
				return _aggregateClause;
			}
		}
		
		private AggregateClause _aggregateClause = null;	
		#endregion
	
		protected override IDbCommand GetInsertCommand() 
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblMenuItemInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.AppMenuItemId.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblMenuItemUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblMenuItemDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.AppMenuItemId);
			p.SourceColumn = ColumnNames.AppMenuItemId;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.AppMenuItemId);
			p.SourceColumn = ColumnNames.AppMenuItemId;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppMenuTypeId);
			p.SourceColumn = ColumnNames.AppMenuTypeId;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppMenuItem);
			p.SourceColumn = ColumnNames.AppMenuItem;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppParentId);
			p.SourceColumn = ColumnNames.AppParentId;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppDisplayOrder);
			p.SourceColumn = ColumnNames.AppDisplayOrder;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppPageId);
			p.SourceColumn = ColumnNames.AppPageId;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppIsActive);
			p.SourceColumn = ColumnNames.AppIsActive;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppCreatedDate);
			p.SourceColumn = ColumnNames.AppCreatedDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppCreatedBy);
			p.SourceColumn = ColumnNames.AppCreatedBy;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppMenuItemTypeID);
			p.SourceColumn = ColumnNames.AppMenuItemTypeID;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
