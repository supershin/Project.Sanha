using System;
using Humanizer.Localisation;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Services
{
	public interface IServiceUnitSave
	{
        void SaveUnitEquipmentSign(CreateTransactionModel model);

        SearchUnitModel UnitModel(int id);

        bool CheckIn(CheckInModel model);

        bool ValidCheckIn(UsingCodeModel model);
    }
}

