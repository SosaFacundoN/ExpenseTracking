using System;
using System.Collections.Generic;
using System.Text;
using ExpenseTracking.Core;
using System.Linq;

namespace ExpenseTracking.Data
{
    public class InMemoryOperationData : IOperationData
    {
        List<Operation> operations;

        public InMemoryOperationData()
        {
            operations = new List<Operation>()
            {
                new Operation {Id = 1, Date = DateTime.Now, Description="Huevos de Pascua", Money = 460, TypeOperation = Operation.OperationType.Sale, Branch = Operation.StoreBranch.Juanimanda},
                new Operation {Id = 2, Date = DateTime.Now, Description="Recibo Pepsi", Money = 5600, TypeOperation = Operation.OperationType.Expense, Branch = Operation.StoreBranch.Juanimanda},
                new Operation {Id = 3, Date = DateTime.Now, Description="Almacen", Money = 80, TypeOperation = Operation.OperationType.Sale, Branch = Operation.StoreBranch.Buffet},
                new Operation {Id = 4, Date = DateTime.Now, Description="Pasteleria", Money = 890, TypeOperation = Operation.OperationType.Sale, Branch = Operation.StoreBranch.Buffet},
                new Operation {Id = 5, Date = DateTime.Now, Description="Comida para perros", Money = 460, TypeOperation = Operation.OperationType.Sale, Branch = Operation.StoreBranch.Juanimanda}
            };
        }

        public IEnumerable<Operation> GetOperationsByDescription(string description = null)
        {
            return from o in operations
                   where string.IsNullOrEmpty(description) || o.Description.StartsWith(description)
                   orderby o.Description
                   select o;
        }

        public Operation GetById(int id)
        {
            return operations.SingleOrDefault(o => o.Id == id);
        }

        public Operation Add(Operation newOperation)
        {
            operations.Add(newOperation);
            newOperation.Id = operations.Max(o => o.Id) + 1;
            return newOperation;
        }
        public int Commit()
        {
            return 0;
        }
    }
}
