using Autofac;

using Xamarin.Forms;
using System;
using System.Reflection;
using System.Linq;
using System.Globalization;


namespace SampleApp.ViewModels.Base
{
    public class ViewModelLocator
    {
        private static ContainerBuilder builder;
        private static IContainer container;


        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator),
            false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnAutoWireChanged);

        private static void OnAutoWireChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");

            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }

        public static bool GetAutoWireViewModel(BindableObject view)
        {
            return (bool)view.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject view, bool value)
        {
            view.SetValue(AutoWireViewModelProperty, value);
        }


        static ViewModelLocator()
        {
            builder = new ContainerBuilder();

           



            //ViewModels
            builder.RegisterType<SampleViewModel>().SingleInstance();
           

            container = builder.Build();
        }

        static void BuildViewModels()
        {
            Assembly assem = typeof(ViewModelLocator).Assembly;
            var viewModels = assem.GetTypes().Where(vm => vm.FullName.EndsWith("ViewModel")); ;
        }
        public static T Resolve<T>()
        {
            using (var scope = container.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }
    }
}
