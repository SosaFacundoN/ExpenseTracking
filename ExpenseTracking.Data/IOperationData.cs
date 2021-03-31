using System;
using System.Collections.Generic;
using System.Text;
using ExpenseTracking.Core;

namespace ExpenseTracking.Data
{
    public interface IOperationData
    {
        IEnumerable<Operation> GetOperationsByDescription(string description);
        Operation GetById(int id);
        Operation Add(Operation newOperation);
        Operation Update(Operation updatedOperation);
        Operation GetByDate(DateTime dateOperation);
        Operation Delete(Operation deletedOperation);
        int Commit();
    }
}
