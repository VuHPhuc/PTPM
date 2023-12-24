using System;
using System.Collections.Generic;

namespace PTPM.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ShipDate { get; set; }

    public int? TransactStatusId { get; set; }

    public bool Deleted { get; set; }

    public bool Paid { get; set; }

    public int? TotalMoney { get; set; }

    public string? Address { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetaill> OrderDetaills { get; set; } = new List<OrderDetaill>();

    public virtual TransactStatus? TransactStatus { get; set; }
}
