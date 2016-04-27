﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UI.Models.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PSSEntities : DbContext
    {
        public PSSEntities()
            : base("name=PSSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CheckDepot> CheckDepot { get; set; }
        public virtual DbSet<CheckDepotDetail> CheckDepotDetail { get; set; }
        public virtual DbSet<Compose> Compose { get; set; }
        public virtual DbSet<ComposeDetail> ComposeDetail { get; set; }
        public virtual DbSet<CustomerLevel> CustomerLevel { get; set; }
        public virtual DbSet<CustomerOrder> CustomerOrder { get; set; }
        public virtual DbSet<CustomerOrderDetail> CustomerOrderDetail { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Depots> Depots { get; set; }
        public virtual DbSet<DepotStock> DepotStock { get; set; }
        public virtual DbSet<DevolveDetail> DevolveDetail { get; set; }
        public virtual DbSet<Devolves> Devolves { get; set; }
        public virtual DbSet<InOutDepot> InOutDepot { get; set; }
        public virtual DbSet<InOutDepotDetail> InOutDepotDetail { get; set; }
        public virtual DbSet<LostDetail> LostDetail { get; set; }
        public virtual DbSet<Losts> Losts { get; set; }
        public virtual DbSet<OtherInDepot> OtherInDepot { get; set; }
        public virtual DbSet<OtherInDepotDetail> OtherInDepotDetail { get; set; }
        public virtual DbSet<OtherOutDepot> OtherOutDepot { get; set; }
        public virtual DbSet<OtherOutDepotDetail> OtherOutDepotDetail { get; set; }
        public virtual DbSet<PayOffDetail> PayOffDetail { get; set; }
        public virtual DbSet<PayOffs> PayOffs { get; set; }
        public virtual DbSet<PopedomRole> PopedomRole { get; set; }
        public virtual DbSet<Popedoms> Popedoms { get; set; }
        public virtual DbSet<ProduceInDepot> ProduceInDepot { get; set; }
        public virtual DbSet<ProduceInDepotDeteil> ProduceInDepotDeteil { get; set; }
        public virtual DbSet<ProduceOutDepot> ProduceOutDepot { get; set; }
        public virtual DbSet<ProduceOutDepotDetail> ProduceOutDepotDetail { get; set; }
        public virtual DbSet<ProductColor> ProductColor { get; set; }
        public virtual DbSet<ProductLend> ProductLend { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductSpec> ProductSpec { get; set; }
        public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<ProductUnit> ProductUnit { get; set; }
        public virtual DbSet<QuotePrice> QuotePrice { get; set; }
        public virtual DbSet<QuotePriceDetail> QuotePriceDetail { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SaleDepot> SaleDepot { get; set; }
        public virtual DbSet<SaleDepotDetail> SaleDepotDetail { get; set; }
        public virtual DbSet<SaleReturn> SaleReturn { get; set; }
        public virtual DbSet<SaleReturnDetail> SaleReturnDetail { get; set; }
        public virtual DbSet<SplitDetail> SplitDetail { get; set; }
        public virtual DbSet<Splits> Splits { get; set; }
        public virtual DbSet<StockDetail> StockDetail { get; set; }
        public virtual DbSet<StockInDepot> StockInDepot { get; set; }
        public virtual DbSet<StockInDepotDetail> StockInDepotDetail { get; set; }
        public virtual DbSet<StockReturn> StockReturn { get; set; }
        public virtual DbSet<StockReturnDetail> StockReturnDetail { get; set; }
        public virtual DbSet<Stocks> Stocks { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersRole> UsersRole { get; set; }
    
        public virtual int myProc(string tbName, string keyFiled, string showFiled, string where, string orderBy, Nullable<int> pageIndex, Nullable<int> pageSize)
        {
            var tbNameParameter = tbName != null ?
                new ObjectParameter("tbName", tbName) :
                new ObjectParameter("tbName", typeof(string));
    
            var keyFiledParameter = keyFiled != null ?
                new ObjectParameter("keyFiled", keyFiled) :
                new ObjectParameter("keyFiled", typeof(string));
    
            var showFiledParameter = showFiled != null ?
                new ObjectParameter("showFiled", showFiled) :
                new ObjectParameter("showFiled", typeof(string));
    
            var whereParameter = where != null ?
                new ObjectParameter("where", where) :
                new ObjectParameter("where", typeof(string));
    
            var orderByParameter = orderBy != null ?
                new ObjectParameter("orderBy", orderBy) :
                new ObjectParameter("orderBy", typeof(string));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("pageIndex", pageIndex) :
                new ObjectParameter("pageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("pageSize", pageSize) :
                new ObjectParameter("pageSize", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("myProc", tbNameParameter, keyFiledParameter, showFiledParameter, whereParameter, orderByParameter, pageIndexParameter, pageSizeParameter);
        }
    
        public virtual int OtherInDepot_proc(string iD)
        {
            var iDParameter = iD != null ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("OtherInDepot_proc", iDParameter);
        }
    
        public virtual int OtherInDepotProc(string id, Nullable<int> userid)
        {
            var idParameter = id != null ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(string));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("OtherInDepotProc", idParameter, useridParameter);
        }
    
        public virtual int pro_cursor()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pro_cursor");
        }
    
        public virtual int pro_order(string tbName, string keyName, string qz, ObjectParameter dD)
        {
            var tbNameParameter = tbName != null ?
                new ObjectParameter("tbName", tbName) :
                new ObjectParameter("tbName", typeof(string));
    
            var keyNameParameter = keyName != null ?
                new ObjectParameter("keyName", keyName) :
                new ObjectParameter("keyName", typeof(string));
    
            var qzParameter = qz != null ?
                new ObjectParameter("qz", qz) :
                new ObjectParameter("qz", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pro_order", tbNameParameter, keyNameParameter, qzParameter, dD);
        }
    
        public virtual int proc_RestoreDataBase()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_RestoreDataBase");
        }
    
        public virtual int proc_SaveDataBase()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_SaveDataBase");
        }
    
        public virtual int sqltest(string columnName)
        {
            var columnNameParameter = columnName != null ?
                new ObjectParameter("columnName", columnName) :
                new ObjectParameter("columnName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sqltest", columnNameParameter);
        }
    
        public virtual int sqltest2(string tbName, ObjectParameter count)
        {
            var tbNameParameter = tbName != null ?
                new ObjectParameter("tbName", tbName) :
                new ObjectParameter("tbName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sqltest2", tbNameParameter, count);
        }
    
        public virtual int StockProc(string iD)
        {
            var iDParameter = iD != null ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("StockProc", iDParameter);
        }
    }
}
