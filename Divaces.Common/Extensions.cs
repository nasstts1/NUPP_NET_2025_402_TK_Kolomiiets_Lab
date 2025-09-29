using System;
using System.Collections.Generic;

namespace Devices.Common {
    public static class Extensions {
        public static void Print<T>(this IEnumerable<T> collection) where T : Device {
            foreach (var item in collection){
                    item.DisplayInfo();
                    Console.WriteLine("--------------------");
            }
        }
    }
}
