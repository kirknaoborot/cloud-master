using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KancelarCloud.Models.HelpDesk;
using System.Data.SqlClient;

namespace KancelarCloud
{
    public class HelpDeskService
    {
        public async void AddHelpDesk (string name, string email,string fio,string uid)
        {

            HelpDeskContext helpDesk = new HelpDeskContext();

            helpDesk.Database.GetDbConnection();
            
            //var comandText = ("INSERT INTO[HelpDesk].[dbo].[Tickets]([Email],[FIO],[Phone],[UsersId],[DepartmentsId],[TopicsId],[Message],[Date],[Close],[CloseDate],[PriorityId],[CreateId],[CloseId],[LateDate]) VALUES ([@Email],[@Fio],[@Phone],[@UserId],[@DepartmentsId],[@TopicsId],[@Message],[@Date],[@Close],[@CloseDate],[@PriorityId],[@CreateId],[@CloseId],[@LateDate])");
            var Email = new SqlParameter("Email",System.Data.SqlDbType.NVarChar).Value=email;
                var Fio = new SqlParameter("Fio",System.Data.SqlDbType.NVarChar).Value=fio;
                var Phone = new SqlParameter("Phone",System.Data.SqlDbType.NVarChar).Value = "280-84-41";
                var UserId = new SqlParameter("@UserId",System.Data.SqlDbType.NVarChar).Value=uid;
                var DepatmentsId = new SqlParameter("@DepartmentsId",System.Data.SqlDbType.Int).Value=1;
                var TopicsId = new SqlParameter("@TopicsId",System.Data.SqlDbType.Int).Value=51;
                var Message = new SqlParameter("@Message",System.Data.SqlDbType.NVarChar).Value = "Переотправка вложения";
                //var Date = new SqlParameter("@Date", DateTime.Now).SqlDbType=System.Data.SqlDbType.DateTime;
                var Close = new SqlParameter("@Close",System.Data.SqlDbType.Bit).Value=1;
                //var CloseDate = new SqlParameter("@CloseDate", DateTime.Now).SqlDbType=System.Data.SqlDbType.DateTime;
                var PriorityId = new SqlParameter("@PrioriryId",System.Data.SqlDbType.Int).Value=1;
                var CreateId = new SqlParameter("@CreateId",System.Data.SqlDbType.NVarChar).Value=uid;
                var CloseId = new SqlParameter("@CloseId", System.Data.SqlDbType.NVarChar).Value=uid;
                //var LateDate = new SqlParameter("@LateDate", DateTime.Now).SqlDbType=System.Data.SqlDbType.DateTime;

            //await helpDesk.Database.ExecuteSqlCommandAsync(comandText, Email, Fio, Phone, UserId, DepatmentsId, TopicsId, Message, Date, Close, CloseDate, PriorityId, CreateId, CloseId, LateDate);
            await helpDesk.Database.ExecuteSqlCommandAsync(String.Format("Exec AddTicket '{0}','{1}','{2}','{3}',{4},{5},'{6}',{7},{8},'{9}','{10}'", Email, Fio, Phone, UserId, Convert.ToInt32(DepatmentsId), Convert.ToInt32(TopicsId), Message,  Convert.ToInt32(Close),  Convert.ToInt32(PriorityId), CreateId, CloseId));


            }



            }
        }
    

