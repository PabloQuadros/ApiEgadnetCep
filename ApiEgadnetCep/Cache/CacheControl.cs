using ApiEgadnetCep.Model;
using System;
using System.Collections.Generic;
using System.Timers;


namespace ApiEgadnetCep.Cache
{
    public static  class CacheControl
    {
        public static List<CepDto> CacheCep = new List<CepDto>();
        private static System.Timers.Timer CacheTimer;
        public static void SetTimer()
        {
            
            CacheTimer = new System.Timers.Timer(300000);
            CacheTimer.Elapsed += OnTimedEvent;
            CacheTimer.AutoReset = true;
            CacheTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            CacheCep.Clear();
        }


    }
}
