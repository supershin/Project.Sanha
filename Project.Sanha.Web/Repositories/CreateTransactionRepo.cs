using System;
using System.Net.Mime;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public class CreateTransactionRepo : ICreateTransactionRepo
	{
        private readonly SanhaDbContext _context;

        public CreateTransactionRepo(SanhaDbContext context)
		{
			_context = context;
		}

		public GetTransModel CreateTransaction(CreateTransactionModel create)
		{
            Sanha_ts_Shopservice_Trans trans = new Sanha_ts_Shopservice_Trans();
            trans.EventID = create.UnitShopId;
            trans.CustomerName = create.CustomerName;
            trans.CustomerMobile = create.CustomerMobile;
            trans.CustomerEmail = create.CustomerEmail;
            trans.CustomerRelationID = create.RelationShip;
            trans.StaffName = create.StaffName;
            trans.WorkDate = DateTime.Parse(create.Date);
            trans.WorkTime = create.StartTime + "-" + create.EndTime;
            trans.Remark = create.Remark;
            trans.CreateDate = DateTime.Now;
            trans.CreateBy = "Application";
            trans.UpdateDate = DateTime.Now;
            trans.UpdateBy = "Application";
            trans.Status = 1;
            trans.UsedQuota = create.UsingQuota;

            _context.Sanha_ts_Shopservice_Trans.Add(trans);
            _context.SaveChanges();

            Sanha_tr_UnitShopservice? unitShop = _context.Sanha_tr_UnitShopservice.Where(o => o.ID == trans.EventID && o.FlagActive == true).FirstOrDefault();
            if(unitShop != null)
            {
                if (unitShop.Quota <= create.UsingQuota) throw new Exception("โควต้าเกินจำนวนคงเหลือ");
                unitShop.UsedQuota = unitShop.UsedQuota + create.UsingQuota;
                unitShop.UpdateDate = DateTime.Now;
                unitShop.UpdateBy = 2;

                _context.Sanha_tr_UnitShopservice.Update(unitShop);
                _context.SaveChanges();
            }

            GetTransModel createTrans = new GetTransModel()
            {
                TransId = trans.ID,
                EventId = (int)trans.EventID,
            };

            return createTrans;
        }

        public bool UploadImage(List<IFormFile> images, int transId, string appPath)
        {
            foreach ( var image in images)
            {
                Guid guidId = Guid.NewGuid();
                
                string fileType = System.IO.Path.GetExtension(image.FileName);
                string fileName = guidId + fileType;
                string contentType = image.ContentType;

                var folder = DateTime.Now.ToString("yyyyMM");
                var dirPath = $"Upload/document/{folder}/images/";
                string filePath = dirPath + fileName;

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), dirPath)))
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), dirPath));
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                Sanha_tr_Shopservice_Resource resourceImage = new Sanha_tr_Shopservice_Resource();
                resourceImage.ID = Guid.NewGuid();
                resourceImage.TransID = transId;
                resourceImage.ResourceType = 1;
                resourceImage.FileName = fileName;
                resourceImage.FilePath = filePath;
                resourceImage.MimeType = contentType;
                resourceImage.FlagActive = true;
                resourceImage.CreateDate = DateTime.Now;
                resourceImage.CreateBy = 1;
                resourceImage.UpdateDate = DateTime.Now;
                resourceImage.UpdateBy = 1;

                _context.Sanha_tr_Shopservice_Resource.Add(resourceImage);
                _context.SaveChanges();
            }

            return true;
        }

        public bool CreateUploadSign(int transId, string fileName, string filePath)
        {
            Sanha_tr_Shopservice_Resource resourceImage = new Sanha_tr_Shopservice_Resource();
            resourceImage.ID = Guid.NewGuid();
            resourceImage.TransID = transId;
            resourceImage.ResourceType = 2;
            resourceImage.FileName = fileName;
            resourceImage.FilePath = filePath;
            resourceImage.MimeType = "image/jpg";
            resourceImage.FlagActive = true;
            resourceImage.CreateDate = DateTime.Now;
            resourceImage.CreateBy = 1;
            resourceImage.UpdateDate = DateTime.Now;
            resourceImage.UpdateBy = 1;

            _context.Sanha_tr_Shopservice_Resource.Add(resourceImage);
            _context.SaveChanges();

            return true;
        }

        public Resources UploadSignResource(string model, string appPath, int transId)
        {
            Resources resource = new Resources();
            Guid guidId = Guid.NewGuid();
            string filePath = "";

            if (model != null)
            {
                string fileName = guidId + ".jpg";

                var folder = DateTime.Now.ToString("yyyyMM");
                var dirPath = $"Upload/document/{folder}/sign/";
                filePath = dirPath + fileName;

                resource.PhysicalPathServer = appPath;
                resource.ResourceStorageBase64 = model;
                resource.ResourceStoragePath = filePath;
                resource.Directory = Path.Combine(appPath, dirPath);
                ConvertByteToImage(resource);

                CreateUploadSign(transId, fileName, filePath);
            }

            return resource;
        }

        private void ConvertByteToImage(Resources item)
        {
            // Convert the Base64 UUEncoded input into binary output. 
            byte[] binaryData;
            try
            {
                binaryData =
                   System.Convert.FromBase64String(item.ResourceStorageBase64);
            }
            catch (System.ArgumentNullException)
            {
                System.Console.WriteLine("Base 64 string is null.");
                return;
            }
            catch (System.FormatException ex)
            {
                throw ex;
            }

            // Write out the decoded data.
            System.IO.FileStream outFile;
            try
            {
                if (!Directory.Exists(item.Directory))
                {
                    Directory.CreateDirectory(item.Directory);
                }
                //var pathFile = string.Format("{0}{1}", item.PhysicalPathServer, item.ResourceStoragePath);
                outFile = new System.IO.FileStream(item.ResourceStoragePath,
                                           System.IO.FileMode.Create,
                                           System.IO.FileAccess.Write);
                outFile.Write(binaryData, 0, binaryData.Length);
                outFile.Close();
            }
            catch (System.Exception exp)
            {
                // Error creating stream or writing to it.
                throw exp;
            }
        }
    }
}

