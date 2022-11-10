// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Inputs;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Models.Components.Containers;
using TheStandardBox.UIKit.Blazor.Services.Views.StandardEdits;
using TheStandardBox.UIKit.Blazor.Views.Components.Localizations;

namespace TheStandardBox.UIKit.Blazor.Views.Components.StandardEdits
{
    public partial class StandardEditComponent<TEntity> : LocalizedComponent
        where TEntity : IStandardEntity
    {
        [Inject]
        public IStandardEditViewService<TEntity> EditViewService { get; set; }

        [Parameter]
        public int Columns { get; set; } = 1;

        protected int ColumnCount => Columns > 0 ? Columns : 1;

        [Parameter]
        public TEntity Entity { get; set; }

        [Parameter]
        public EventCallback<TEntity> EntityChanged { get; set; }

        // private List<IViewElement> textViewElements;

        private string AddErrorMessage { get; set; }

        protected override void OnInitialized()
        {
            if (Entity == null)
            {
                Entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
                Entity.Id = Guid.NewGuid();
            }

            //textViewElements = EditViewService.GenerateViewElements(Entity);

            State = ComponentState.Content;
        }

        private async void OnAddClicked()
        {
            try
            {
                AddErrorMessage = string.Empty;

                //  await EditViewService.UpsertEntityAsync(Entity, textViewElements);
            }
            catch (Exception ex)
            {
                AddErrorMessage = ex.Message;
            }
            StateHasChanged();
        }

        public RenderFragment CreateComponent() => builder =>
        {
            var proList = typeof(TEntity).GetProperties();
            foreach (var prp in proList)
            {
                Type T = prp.GetType();
                if (prp.GetCustomAttributes(typeof(DataTypeAttribute), false).Length != 0)
                {
                    var attrList = (DataTypeAttribute)prp.GetCustomAttributes(typeof(DataTypeAttribute), false).First();
                    var displayLabel = (DisplayAttribute)prp.GetCustomAttributes(typeof(DisplayAttribute), false).First();
                    // Get the initial property value.
                    var propInfoValue = typeof(TEntity).GetProperty(prp.Name);
                    // Create an expression to set the ValueExpression-attribute.
                    var constant = System.Linq.Expressions.Expression.Constant(Entity, typeof(TEntity));
                    var exp = System.Linq.Expressions.MemberExpression.Property(constant, prp.Name);
                    switch (attrList.DataType)
                    {
                        case DataType.Text:
                        case DataType.EmailAddress:
                        case DataType.PhoneNumber:
                        case DataType.MultilineText:
                        case DataType.Password:
                            {
                                builder.OpenComponent(0, typeof(SfTextBox));
                                // Create the handler for ValueChanged.
                                builder.AddAttribute(3, "ValueChanged", RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.EventCallback.Factory.CreateInferred(this, __value => propInfoValue.SetValue(Entity, __value), (string)propInfoValue.GetValue(Entity)))));
                                builder.AddAttribute(4, "ValueExpression", System.Linq.Expressions.Expression.Lambda<Func<string>>(exp));
                                if (attrList.DataType == DataType.MultilineText)
                                    builder.AddAttribute(5, "Multiline", true);

                                if (attrList.DataType == DataType.Password)
                                    builder.AddAttribute(6, "Type", InputType.Password);
                                break;
                            }
                        case DataType.Date:
                            builder.OpenComponent(0, typeof(SfDatePicker<DateTimeOffset>));
                            builder.AddAttribute(3, "ValueChanged", RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<DateTimeOffset>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<DateTimeOffset>(this, Microsoft.AspNetCore.Components.EventCallback.Factory.CreateInferred(this, __value => propInfoValue.SetValue(Entity, __value), (DateTimeOffset)propInfoValue.GetValue(Entity)))));
                            builder.AddAttribute(4, "ValueExpression", System.Linq.Expressions.Expression.Lambda<Func<DateTimeOffset>>(exp));
                            break;
                        case DataType.Duration:
                            builder.OpenComponent(0, typeof(SfNumericTextBox<decimal?>));
                            builder.AddAttribute(3, "ValueChanged", RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<decimal?>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<decimal?>(this, Microsoft.AspNetCore.Components.EventCallback.Factory.CreateInferred(this, __value => propInfoValue.SetValue(Entity, __value), (decimal?)propInfoValue.GetValue(Entity)))));
                            builder.AddAttribute(4, "ValueExpression", System.Linq.Expressions.Expression.Lambda<Func<decimal?>>(exp));
                            break;
                        case DataType.Custom:
                            //if (attrList.CustomDataType == "DropdownList")
                            //{
                            //    builder.OpenComponent(0, typeof(Syncfusion.Blazor.DropDowns.SfDropDownList<string, Countries>));
                            //    builder.AddAttribute(1, "DataSource", countries.GetCountries());
                            //    builder.AddAttribute(4, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((builder2) =>
                            //    {
                            //        builder2.AddMarkupContent(5, "\r\n    ");
                            //        builder2.OpenComponent<Syncfusion.Blazor.DropDowns.DropDownListFieldSettings>
                            //      (6);

                            //        builder2.AddAttribute(7, "Value", "Code");
                            //        builder2.AddAttribute(8, "Text", "Name");
                            //        builder2.CloseComponent();
                            //        builder2.AddMarkupContent(9, "\r\n");
                            //    }));

                            //}
                            //else if (attrList.CustomDataType == "ComboBox")
                            //{
                            //    builder.OpenComponent(0, typeof(Syncfusion.Blazor.DropDowns.SfComboBox<string, Cities>));
                            //    builder.AddAttribute(1, "DataSource", cities.GetCities());
                            //    builder.AddAttribute(4, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((builder2) =>
                            //    {
                            //        builder2.AddMarkupContent(5, "\r\n    ");
                            //        builder2.OpenComponent<Syncfusion.Blazor.DropDowns.ComboBoxFieldSettings>
                            //      (6);

                            //        builder2.AddAttribute(7, "Value", "Code");
                            //        builder2.AddAttribute(8, "Text", "Name");
                            //        builder2.CloseComponent();
                            //        builder2.AddMarkupContent(9, "\r\n");
                            //    }));
                            //}
                            //builder.AddAttribute(3, "ValueChanged", RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.EventCallback.Factory.CreateInferred(this, __value => propInfoValue.SetValue(Entity, __value), (string)propInfoValue.GetValue(Entity)))));
                            //builder.AddAttribute(4, "ValueExpression", System.Linq.Expressions.Expression.Lambda<Func<string>>(exp));
                            break;
                        default:
                            break;
                    }
                    var defaultValue = propInfoValue.GetValue(Entity);
                    builder.AddAttribute(1, "Value", defaultValue);
                    builder.AddAttribute(6, "PlaceHolder", displayLabel.Name);
                    builder.AddAttribute(6, "FloatLabelType", FloatLabelType.Auto);

                    builder.CloseComponent();
                }
            }
        };
    }
}
