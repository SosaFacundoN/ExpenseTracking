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
    public class ServicesModel : PageModel
    {
        public ServicesModel(IConfiguration config, IOperationData operationData, IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.config = config;
            this.operationData = operationData;
        }
        private readonly IHtmlHelper htmlHelper;
        private readonly IConfiguration config;
        private IOperationData operationData;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IEnumerable<Operation> Services { get; set; }
        public IEnumerable<SelectListItem> Type { get; set; }
        public IEnumerable<SelectListItem> Pay { get; set; }
        [BindProperty]
        public Operation Service { get; set; }

        public IActionResult OnGet(int? operationId)
        {
            Services = operationData.GetOperationsByDescription(SearchTerm);
            Type = htmlHelper.GetEnumSelectList<Operation.OperationType>();
            Pay = htmlHelper.GetEnumSelectList<Operation.PaymentMethod>();

            if (operationId.HasValue && Service.IsService == true)
            {
                    Service = operationData.GetById(operationId.Value);

                    operationData.Delete(Service);
                    return Page();
            }
            else
            {
                Service = new Operation();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            int id = 0;
            id = Service.Id;
            id = id++;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Service.Id < id)
            {
                operationData.Update(Service);
            }
            else
            {
                operationData.Add(Service);
                Service.IsService = true;
            }
            operationData.Commit();
            return RedirectToPage("./Services");


            /*if(Operation.Id > 0)
            {
                operationData.Update(Operation);
                operationData.Commit();
                return Page();
            }
            else
            {
                operationData.Add(Operation);
                operationData.Commit();
                return RedirectToPage("./CurrentDay", new { operationId = Operation.Id });
            }*/
        }

    }
}
