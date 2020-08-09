using Exception;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Exception
{
    public abstract class IntegrationFieldsValidator : MonoBehaviour
    {
        public bool IsValid => ValidateFields();
        public bool IsNotValid => !ValidateFields();
        protected abstract List<(object, string)> FieldsToBeValidated();

        private bool ValidateFields()
        {
            var fieldsValidations = new List<bool>();

            foreach (var (field, fieldName) in FieldsToBeValidated())
            {
                fieldsValidations.Add(IsFilled(field, fieldName));
            }

            return fieldsValidations.TrueForAll(isTrue => isTrue);
        }

        private bool IsFilled(object field, string fieldName)
        {
            if (field is null)
            {
                Debug.LogError(BaseException.FieldNotSetted(fieldName, gameObject.name));
                return false;
            }

            return true;
        }
    }
}
