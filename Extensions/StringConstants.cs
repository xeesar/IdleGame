namespace Extensions
{
    public class StringConstants
    {
        public static class PlayerPrefsKeys
        {
            public static readonly string CitiesData                      = "cities_data";
            public static readonly string Dollar                          = "dollar";
            public static readonly string Respect                         = "respect";

            public static readonly string FirstSession                    = "first_session";
            public static readonly string TutorialStage                   = "tutorial_stage";

            public static readonly string IsSoundOn                       = "is_sound_on";
            public static readonly string IsMusicOn                       = "is_music_on";
        }


        public static class Formats
        {
            public static readonly string LevelFormat                     = "level {0}";
            public static readonly string ProgressFormat                  = "{0}%";
            public static readonly string PlusIncomeFormat                = "+{0}";
            
            public static readonly string IncomeRespectFormat             = "{0} RP";
            public static readonly string IncomeDollarsFormat             = "{0} DL";
            public static readonly string ProductionSpeedFormat           = "{0} SEC";

            public static readonly string AddArtistFormat                 = "Add Artist ({0}/{1})";
            public static readonly string ArtistsFormat                   = "{0}/{1}";
            
            public static readonly string ScreenShotNameFormat            = "graffiti_idle_{0}.png";
            
            public static readonly string UnlockDistrictFormat            = "to unlock the district, \npaint {0} graffiti";
            
            public static readonly string OfflineIncomeFormat             = "<color=#3F3D3C>While you were away for {0}\n<color=#E17237>[MAX {1} hours]<color=#3F3D3C>,  you earned";
            
            public static readonly string AdUpgradeParameterFormat        = "upgrade_{0}";
        }
        
        
        public static class AnalyticsEvents
        {
            public static readonly string HouseTapped                     = "house_tapped";
            public static readonly string HouseUnlocked                   = "house_unlocked";
            public static readonly string HouseOpened                     = "house_opened";

            public static readonly string DrawnGraffiti                   = "graffiti_is_drawn";
            
            public static readonly string IncomeCollected                 = "income_collected";
            public static readonly string UnlockAutoCollect               = "auto_collect_income_bought";

            public static readonly string UpgradeDynamicParameter         = "upgrade_dynamic_parameter";
            public static readonly string UpgradeProductionBuilding       = "upgrade_production_building";
        }
        
        
        public static class AnalyticsEventsParameters
        {
            public static readonly string City                            = "city";
            
            public static readonly string BuildingId                      = "building_id";
            public static readonly string HouseType                       = "houseType";
            public static readonly string CurrencyType                    = "currency_type";

            public static readonly string CountOfArtists                  = "count_of_artists";
            public static readonly string MorePerBlock                    = "more_per_block";
            public static readonly string RunningSpeed                    = "running_speed";
            public static readonly string DrawingSpeed                    = "drawing_speed";
            public static readonly string CapacityOfCans                  = "capacity_of_cans";
            
            public static readonly string ParameterType                   = "parameter_type";
        }
        
        
        
        public static class AnalyticsEventsValues
        {
            public static readonly string Locked                         = "locked";
            public static readonly string InProgress                     = "in_progress";
            public static readonly string Done                           = "done";
        }
        
        
        public static class  UIText
        {
            public static readonly string MaxLevel                        = "MAX";
            public static readonly string Unlocked                        = "UNLOCKED";
        }
        
        
        public static class AnimationClipNames
        {
            public static readonly string TakeFromTable                 = "to_take_ballon_off_table";
        }
    }
}
