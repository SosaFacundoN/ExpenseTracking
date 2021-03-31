using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpenseTracking.Core
{
    public partial class Operation
    {

        public int Id { get; set; }
        [Required]
        public decimal Money { get; set; }
        public DateTime Date { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }
        [Required]
        public OperationType TypeOperation { get; set; }
        [Required]
        public StoreBranch Branch { get; set; }
        public PaymentMethod Payment { get; set; }
        public bool IsService { get; set; }
    }
}
