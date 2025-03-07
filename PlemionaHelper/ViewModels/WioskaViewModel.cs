﻿using PlemionaHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_ThirdParty;

namespace PlemionaHelper.ViewModels
{
    public class WioskaViewModel : BaseViewModel
    {
        public Wioska Wioska { get; }

        public WioskaViewModel(Wioska wioska)
        {
            Wioska = wioska;
        }

        public int StanGlina => Math.Min(Zasoby_MaxIlosc,
            EnvironmentCalculators.ObliczAktualnyStanZasobu(
                Wioska.PoziomCegla,
                Wioska.OstatniAtak.CzasAtaku,
                Wioska.OstatniAtak.WyszpiegowanaGlina));

        public int StanDrewno => Math.Min(Zasoby_MaxIlosc,
            EnvironmentCalculators.ObliczAktualnyStanZasobu(
                Wioska.PoziomTartak,
                Wioska.OstatniAtak.CzasAtaku,
                Wioska.OstatniAtak.WyszpiegowaneDrewno));

        public int StanZelazo => Math.Min(Zasoby_MaxIlosc,
            EnvironmentCalculators.ObliczAktualnyStanZasobu(
                Wioska.PoziomZelazo,
                Wioska.OstatniAtak.CzasAtaku,
                Wioska.OstatniAtak.WyszpiegowaneZelazo));

        public DateTime DrewnoOverflowDateTime
        {
            get
            {
                if (Zasoby_MaxIlosc == StanDrewno)
                    return DateTime.Now;

                int pozostaloMiejsca = Zasoby_MaxIlosc - StanDrewno;

                decimal wydobycieZasobuPerMinute = EnvironmentCalculators.
                    KopalniaLvlToGodzinneWydobycie(Wioska.PoziomTartak) / 60m;

                return DateTime.Now.AddMinutes((double)(pozostaloMiejsca / wydobycieZasobuPerMinute));
            }
        }

        public DateTime GlinaOverflowDateTime
        {
            get
            {
                if (Zasoby_MaxIlosc == StanGlina)
                    return DateTime.Now;

                int pozostaloMiejsca = Zasoby_MaxIlosc - StanGlina;

                decimal wydobycieZasobuPerMinute = EnvironmentCalculators.
                    KopalniaLvlToGodzinneWydobycie(Wioska.PoziomCegla) / 60m;

                return DateTime.Now.AddMinutes((double)(pozostaloMiejsca / wydobycieZasobuPerMinute));
            }
        }

        public DateTime ZelazoOverflowDateTime
        {
            get
            {
                if (Zasoby_MaxIlosc == StanZelazo)
                    return DateTime.Now;

                int pozostaloMiejsca = Zasoby_MaxIlosc - StanZelazo;

                decimal wydobycieZasobuPerMinute = EnvironmentCalculators.
                    KopalniaLvlToGodzinneWydobycie(Wioska.PoziomZelazo) / 60m;

                return DateTime.Now.AddMinutes((double)(pozostaloMiejsca / wydobycieZasobuPerMinute));
            }
        }

        private int _zasoby_MaxIlosc { get; set; }
        public int Zasoby_MaxIlosc
        {
            get
            {
                if (_zasoby_MaxIlosc == 0)
                    _zasoby_MaxIlosc = EnvironmentCalculators.SpichlerzLvlToSpichlerzPojemnosc(Wioska.PoziomSpichlerza) -
                        EnvironmentCalculators.SchowekLvlToSchowekPojemnosc(Wioska.PoziomSchowka);
                return _zasoby_MaxIlosc;
            }
        }
            
        public void Refresh()
        {
            OnPropertyChanged(nameof(StanDrewno));
            OnPropertyChanged(nameof(StanGlina));
            OnPropertyChanged(nameof(StanZelazo));
            OnPropertyChanged(nameof(DrewnoOverflowDateTime));
            OnPropertyChanged(nameof(GlinaOverflowDateTime));
            OnPropertyChanged(nameof(ZelazoOverflowDateTime));
        }
    }
}
