using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpenseTracking.Core;
using ExpenseTracking.Data;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracking.Pages.AppPages
{
    public class CurrentDayModel : PageModel
    {
        public CurrentDayModel(IConfiguration config, IOperationData operationData, IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.config = config;
            this.operationData = operationData;
        }
        private readonly IHtmlHelper htmlHelper;
        private readonly IConfiguration config;
        private IOperationData operationData;

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }
        public IEnumerable<Operation> Operations { get; set; }
        public IEnumerable<SelectListItem> Type { get; set; }
        [BindProperty]
        public Operation Operation { get; set; }


        public decimal GetCurrentDayTotal()
        {
            decimal total = 0;
            foreach(var operation in Operations)
            {
                if (operation.TypeOperation == Operation.OperationType.Expense)
                {
                    total = total - operation.Money;
                }
                else
                    total = total + operation.Money;
            }
            return total;
        }

        public IActionResult OnGet(int? operationId)
        {
            Operations = operationData.GetOperationsByDescription(SearchTerm);
            Type = htmlHelper.GetEnumSelectList<Operation.OperationType>();

            if (operationId.HasValue)
            {
                Operation = operationData.GetById(operationId.Value);
                return Page();
            }
            else
                return Page();
        }

        public IActionResult OnPost(int? operationId)
        {
            if (operationId == null)
            {
                Operation = new Operation();
                operationData.Add(Operation);
            }
            operationData.Commit();
            return Page();
        }
    }
}
