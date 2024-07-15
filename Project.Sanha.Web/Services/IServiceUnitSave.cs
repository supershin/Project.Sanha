using System;
using Humanizer.Localisation;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Services
{
	public interface IServiceUnitSave
	{
        public void SaveUnitEquipmentSign(CreateTransactionModel model);

        SearchUnitModel UnitModel(int id);
    }
}

