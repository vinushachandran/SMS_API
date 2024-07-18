using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModel.RepositoryResponse
{
    public class RepositoryResponse<T>
    {
        
        public T? Data { get; set; }

        public bool Success { get; set; } = true;

        public List<string> Message { get; set; } = new List<string>();

        public string TotalMessages
        {
            get
            {
                var msg = "";
                if (Message.Count > 0)
                {

                    foreach (var item in Message)
                    {
                        msg = item.ToString() + "\n";
                    }
                    return msg;
                }
                return msg;
            }
        }

        public int TotalPages { get; set; }
    }
}

