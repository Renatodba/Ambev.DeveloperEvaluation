using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Representa um item dentro de uma venda
    /// </summary>
    public class SaleItem
    {
        public Guid Id { get; private set; }

        /// <summary>
        /// Identificador externo do produto (External Identity)
        /// </summary>
        public string ProductExternalId { get; private set; }

        /// <summary>
        /// Descrição denormalizada do produto
        /// </summary>
        public string ProductDescription { get; private set; }

        /// <summary>
        /// Quantidade vendida
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Preço unitário
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Percentual de desconto aplicado (ex.: 0.10 para 10%)
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Indica se o item foi cancelado
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Valor total do item considerando quantidade, preço e desconto
        /// </summary>
        public decimal TotalAmount => Math.Round(Quantity * UnitPrice * (1 - Discount), 2);

        protected SaleItem() { }

        public SaleItem(Guid id, string productExternalId, string productDescription, int quantity, decimal unitPrice, decimal discount)
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
        /// Cancela este item de venda
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}

