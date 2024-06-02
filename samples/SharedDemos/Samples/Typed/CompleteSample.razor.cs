using SharedDemos.Samples.Models;
using BlazorDynamics.Forms.Components.Factories;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Common.Enums;

namespace SharedDemos.Samples.Typed
{
    public partial class CompleteSample : SampleBasePage
    {
        private void update()
        {
            StateHasChanged();
        }

        private DynamicFormModel model;

        protected override void OnInitialized()
        {
            model = FormFactory.GroupLayout(
                FormFactory.StringComponent("First Name", "$.FirstName"),
                FormFactory.StringComponent("Last Name", "$.LastName"),
                FormFactory.DateTimeComponent("Birth Date", "$.BirthDate"),
                FormFactory.BooleanComponent("Is address Active", "$.Address.IsActive"),
                CreateAddressForm(),
                FormFactory.AddAction("+ car", "$.Cars", new Car()),
                FormFactory.NumberDisplay("Cars:", "$.Cars"),
                CreateCarsForm()
            );
        }

        private DynamicFormModel CreateAddressForm()
        {
            return FormFactory.GroupLayout(
                FormFactory.StringComponent("Street Name", "$.Address.StreetName"),
                FormFactory.DropDownComponent("City", "$.Address.City", DataFactory.SelectionList(new List<string> { "Utrecht", "Amsterdam", "Den Helder" }
                )
            )).AddSchemaRule(RuleEffect.HIDE, "#/properties/Address/properties/IsActive", "{ const: false }");
        }

      

        private DynamicFormModel CreateCarsForm()
        {
            return FormFactory.ListComponent("Cars", "$.Cars",
                CarDetailForm()
            );
        }

        private DynamicFormModel CarDetailForm()
        {
            return FormFactory.GroupLayout(
                                FormFactory.HorizontalLayout(
                                    FormFactory.StringComponent("Make", "@.Make"),
                                    FormFactory.StringComponent("Model", "@.Model"),
                                    FormFactory.IntComponent("Rating", "@.Rating").WithTypeDefinionName("starRating"),
                                    FormFactory.DeleteAction("Delete Car", "@")
                                ),
                                FormFactory.AddAction("+ Comment", "@.Comments", new Comment()),
                                FormFactory.NumberDisplay("Comments:", "@.Comments"),
                                CreateCommentsForm()
                            );
        }

        private DynamicFormModel CreateCommentsForm()
        {
            return               
                FormFactory.ListComponent("Comments", "@.Comments",
                FormFactory.HorizontalLayout(
                    FormFactory.StringComponent("Comment Text", "@.Text"),
                    FormFactory.DeleteAction("Delete", "@")
                )
            );
        }

        private Person data = new Person()
        {
            BirthDate = new DateTime(1995, 06, 29),
            FirstName = "John",
            LastName = "Doe",
            Address = new Address
            {
                City = "Amsterdam",
                PostalCode = "1010 AA",
                Region = "Netherlands",
                StreetName = "kabouterlaan",
                Number = 21,
                IsActive = true
            },
            Cars = new List<Car>()
            {
                new Car(){ Rating= 3, Make = "Citroen" ,Model = "C4 cactus", Comments = new List<Comment>{ new Comment() { Text = " wowo" } , new Comment() { Text = " jaja" } } },
                new Car(){ Rating= 5, Make = "Mazda" ,Model = "3" },
            }
        };
    }



}
