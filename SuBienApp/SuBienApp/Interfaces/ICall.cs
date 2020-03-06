using SuBienApp.Models;
using System.Collections.Generic;

namespace SuBienApp.Interfaces
{
    public interface  ICall
    {
        List<Calls> CallsLog { get; }
    }
}
