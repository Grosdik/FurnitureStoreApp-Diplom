//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FurnitureStoreApp.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class IncomingGoods
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int Product { get; set; }
        public Nullable<int> Supplier { get; set; }
        public int Amount { get; set; }
        public int UnitPrice { get; set; }
        public int PurchaseAmount { get; set; }
    
        public virtual Products Products { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}
