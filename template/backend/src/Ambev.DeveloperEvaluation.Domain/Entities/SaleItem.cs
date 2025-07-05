using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item within a sale.
    /// </summary>
    public class SaleItem
    {
        /// <summary>
        /// Unique identifier for the sale item.
        /// </summary>
        public Guid Id { get; private set; }

        public string ProductExternalId { get; private set; }
        public string ProductDescription { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public bool IsCancelled { get; private set; }

        public decimal TotalAmount => Math.Round(Quantity * UnitPrice * (1 - Discount), 2);

        // Parameterless constructor for ORM
        protected SaleItem() { }

        /// <summary>
        /// Initializes a new sale item with required fields.
        /// </summary>
        public SaleItem(Guid id, string productExternalId, string productDescription,
                        int quantity, decimal unitPrice, decimal discount)
        {
            Id = id;
            ProductExternalId = productExternalId ?? throw new ArgumentNullException(nameof(productExternalId));
            ProductDescription = productDescription ?? throw new ArgumentNullException(nameof(productDescription));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            Quantity = quantity;
            if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice));
            UnitPrice = unitPrice;
            if (discount < 0 || discount > 1) throw new ArgumentOutOfRangeException(nameof(discount));
            Discount = discount;
        }

        /// <summary>
        /// Cancels this sale item.
        /// </summary>
        public void Cancel() => IsCancelled = true;

        /// <summary>
        /// Updates core fields of this sale item based on another instance.
        /// </summary>
        public void UpdateFrom(SaleItem updated)
        {
            if (updated == null) throw new ArgumentNullException(nameof(updated));
            Quantity = updated.Quantity;
            UnitPrice = updated.UnitPrice;
            Discount = updated.Discount;
        }
    }
}

