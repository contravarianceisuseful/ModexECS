using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using UnityEngine;
using Object = System.Object;

namespace ModexECS
{
    public delegate void OnComponentModified(IComponent componentModified, Entity entity);

    public partial class Modex
    {
        private void AttachModifierMethodsToNewEntity(Entity newEntity)
        {
            foreach (IModifySystem system in systems.FindAll(x => x is IModifySystem))
            {
                newEntity.OnComponentModified += system.OnModify;
            }
        }
    }

    public partial class Entity
    {
        public OnComponentModified OnComponentModified;

        public Entity ModifyComponent<T>(T newValuesComponent) where T : IModifiableComponent
        {          
            var oldComponent = GetComponent<T>();    
              
            TransplantFieldValues(newValuesComponent, oldComponent);

            if (OnComponentModified != null)
                OnComponentModified.Invoke(newValuesComponent, this);
            return this;
        }

        private void TransplantFieldValues<T>(T newComponent, T oldComponent) where T : IModifiableComponent
        {
#region fields
            var oldfields = oldComponent.GetType().GetFields(BindingFlags.Public);
            var newFields = newComponent.GetType().GetFields(BindingFlags.Public);
            foreach (FieldInfo fieldInfo in oldfields)
            {
                fieldInfo.SetValue(oldComponent, fieldInfo.GetValue(newComponent));
            }
            #endregion
            #region properties
            var oldProps = oldComponent.GetType().GetProperties();
            var newProps = newComponent.GetType().GetProperties();
            foreach (PropertyInfo propInfo in oldProps)
            { 
                //TODO fix this crazy business
                var indexInfo = propInfo.GetIndexParameters();
                propInfo.SetValue(oldComponent, propInfo.GetValue(newComponent, indexInfo), indexInfo);
            }
            #endregion
        }

        public Entity ModifyComponent<T, T2>(string fieldName, T2 newFieldValue) where T : IModifiableComponent, new()
        {
            var componentToModify = GetComponent<T>();

            var fieldInfos = typeof(T).GetFields(BindingFlags.Public);
            var propInfos = typeof(T).GetProperties();

            var fieldToModify = fieldInfos.FirstOrDefault(x => x.Name == fieldName);
            var propToModify = propInfos.FirstOrDefault(x => x.Name == fieldName);

            #region fields
            if (fieldToModify != null)
            {
                if (fieldToModify.FieldType != typeof(T2))
                {
                    throw new ModexException("named field is not of type " + typeof(T2));
                }
                fieldToModify.SetValue(componentToModify, newFieldValue);
            }
            #endregion

            #region properties
            else if (propToModify != null)
            {
                if (propToModify.PropertyType != typeof(T2))
                {
                    throw new ModexException("named property is not of type " + typeof(T2));
                }
                propToModify.SetValue(componentToModify, newFieldValue, new object[0]);
            }
            #endregion
            else
            {
                throw new ModexException("Cannot find field with name or property with name: " + fieldName);
            }


            if (OnComponentModified != null)
                OnComponentModified.Invoke(componentToModify, this);
            return this;
        }

        public Entity ModifyComponent<T>(Object newFieldValue) where T : IModifiableComponent
        {
            var componentToModify = GetComponent<T>();
            var fieldToModify = typeof(T).GetFields(BindingFlags.Public).FirstOrDefault(x => x.FieldType.IsInstanceOfType(newFieldValue));
            var propToModify = typeof(T).GetProperties().FirstOrDefault(x => x.PropertyType.IsInstanceOfType(newFieldValue));

            #region fields
            if (fieldToModify != null)
            {
                fieldToModify.SetValue(componentToModify, newFieldValue);
            }
            #endregion

            #region properties
            else if (propToModify != null)
            {
                propToModify.SetValue(componentToModify, newFieldValue, new object[0]);
            }
            #endregion
            else
            {
                throw new ModexException("Cannot find field with name or property of type " + newFieldValue.GetType());
            }

            if (OnComponentModified != null)
                OnComponentModified.Invoke(componentToModify, this);
            return this;
        }
    }

}
