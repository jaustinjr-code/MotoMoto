using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class AuthorizationLevel
    {

        private string? typeName;
        private string? featureName;
        
        private bool featureFound;

        private string? errorMessage;

        public AuthorizationLevel()
        {
            TypeName = null;
            FeatureName = null;
            featureFound = false;
            errorMessage = "Authorization Level Could Not Be Found";
        }

        public AuthorizationLevel(string typeName, string featureName, bool featureFound)
        {
            TypeName = typeName;
            FeatureName = featureName;
            FeatureFound = featureFound;
            if (!featureFound) {
                errorMessage = "Authorization Level Could Not Be Found";
            } 
            else {
                errorMessage = null;
            }
        }
    
        public string? TypeName 
        {
            get 
            {
                if (typeName is not null)
                {
                    if (typeName.Trim().Length == 0) 
                    {
                        return null;
                    }
                    return typeName.ToLower().Trim();
                }
                return typeName;
                
            }
            set
            {
                typeName = value;
            }
        }

        public string? FeatureName 
        {
            get 
            {
                if (featureName is not null)
                {
                    if (featureName.Trim().Length == 0) 
                    {
                        return null;
                    }
                    return featureName.ToLower().Trim();
                }
                return featureName;
            }
            set
            {
                featureName = value;
            }
        }

        public bool FeatureFound
        {
            get
            {
                return featureFound;
            }
            set
            {
                featureFound = value;
            }
        }
    }
}
