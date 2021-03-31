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
                new Operation {Id = 1, Date = DateTime.Now, Description="Huevos de Pascua", Money = 460, TypeOperation = Operation.OperationType.Sale, Branch = Operation.StoreBranch.Juanimanda, IsService=false},
                new Operation {Id = 2, Date = DateTime.Now, Description="Recibo Pepsi", Money = 23000, TypeOperation = Operation.OperationType.Expense, Branch = Operation.StoreBranch.Juanimanda, IsService=false},
                new Operation {Id = 3, Date = DateTime.Now, Description="Almacen", Money = 80, TypeOperation = Operation.OperationType.Sale, Branch = Operation.StoreBranch.Buffet, IsService=false},
                new Operation {Id = 4, Date = DateTime.Now, Description="Pasteleria", Money = 890, TypeOperation = Operation.OperationType.Sale, Branch = Operation.StoreBranch.Buffet, IsService=false},
                new Operation {Id = 5, Date = DateTime.Now, Description="Alquiler", Money = 15000, TypeOperation = Operation.OperationType.Expense, Branch = Operation.StoreBranch.Buffet, IsService=true},
                new Operation {Id = 6, Date = DateTime.Now, Description="Comida para perros", Money = 460, TypeOperation = Operation.OperationType.Sale, Branch = Operation.StoreBranch.Juanimanda, IsService=false}
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
            newOperation.Date = DateTime.Now;
            return newOperation;
        }
        public Operation Update(Operation updatedOperation)
        {
            var operation = operations.SingleOrDefault(o => o.Id == updatedOperation.Id);
            if(operation != null)
            {
                operation.Money = updatedOperation.Money;
                operation.Description = updatedOperation.Description;
                operation.TypeOperation = updatedOperation.TypeOperation;
            }
            return operation;
        }
        public Operation Delete(Operation deletedOperation)
        {
            var operation = operations.SingleOrDefault(o => o.Id == deletedOperation.Id);
            operations.Remove(operation);
            return deletedOperation;
        }

        public Operation GetByDate(DateTime dateOperation)
        {
            return operations.SingleOrDefault(o => o.Date == dateOperation);
        }
        public int Commit()
        {
            return 0;
        }
    }
}
