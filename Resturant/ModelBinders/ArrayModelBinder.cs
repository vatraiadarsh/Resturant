using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;

namespace Api.ModelBinders
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // if the model is not an enumerable type
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                // set the result to failed
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            // get the inputted value through the value provider
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

            // if that value is null or whitespace
            if (string.IsNullOrWhiteSpace(value))
            {
                // set the result to succeeded
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            // get the type of the enumerable
            var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];

            // convert each item in the value list to the enumerable type
            var converter = TypeDescriptor.GetConverter(elementType);

            // convert each item to the enumerable type and set it to the values object array
            var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim())).ToArray();

            // create an array of that type and set it's values to the object array we created
            var typedValues = Array.CreateInstance(elementType, values.Length);
            values.CopyTo(typedValues, 0);

            // set the model value with the typed values we just created
            bindingContext.Model = typedValues;

            // set a successful result of our binding
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);

            return Task.CompletedTask;
        }

    }
}
