using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

 /// <summary>
    /// Representa uma venda realizada na loja
    /// </summary>
    public class Sale
    {
        public Guid Id { get; private set; }

        /// <summary>
        /// Número da venda (externo ao sistema, exibido ao usuário)
        /// </summary>
        public string SaleNumber { get; private set; }

        /// <summary>
        /// Data em que a venda foi efetuada
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Identificador externo do cliente (External Identity)
        /// </summary>
        public string CustomerExternalId { get; private set; }

        /// <summary>
        /// Identificador externo da filial onde a venda ocorreu
        /// </summary>
        public string BranchExternalId { get; private set; }

        /// <summary>
        /// Itens que compõem a venda
        /// </summary>
        private readonly List<SaleItem> _items = new List<SaleItem>();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        /// <summary>
        /// Indica se a venda foi cancelada
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Valor total calculado da venda
        /// </summary>
        public decimal TotalAmount => Items.Sum(i => i.TotalAmount);

    // Parameterless constructor for ORM
    protected Sale() { }

    /// <summary>
    /// Initializes a new sale with required fields.
    /// </summary>
    public Sale(Guid id, string saleNumber, DateTime date, string customerExternalId, string branchExternalId)
    {
        Id = id;
        SaleNumber = saleNumber ?? throw new ArgumentNullException(nameof(saleNumber));
        Date = date;
        CustomerExternalId = customerExternalId ?? throw new ArgumentNullException(nameof(customerExternalId));
        BranchExternalId = branchExternalId ?? throw new ArgumentNullException(nameof(branchExternalId));
    }

    /// <summary>
    /// Adds an item to the sale.
    /// </summary>
    public void AddItem(SaleItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        _items.Add(item);
    }

    /// <summary>
    /// Cancels the entire sale.
    /// </summary>
    public void Cancel() => IsCancelled = true;

    /// <summary>
    /// Cancels a specific item within the sale.
    /// </summary>
    public void CancelItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null) throw new InvalidOperationException("Sale item not found.");
        item.Cancel();
    }

    /// <summary>
    /// Updates core fields of this sale based on another instance.
    /// </summary>
    public void UpdateFrom(Sale updated)
    {
        if (updated == null) throw new ArgumentNullException(nameof(updated));
        SaleNumber = updated.SaleNumber;
        Date = updated.Date;
        CustomerExternalId = updated.CustomerExternalId;
        BranchExternalId = updated.BranchExternalId;
        if (updated.IsCancelled) Cancel();
    }
}