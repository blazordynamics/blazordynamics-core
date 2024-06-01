# sample

## Razor declarations
````html
  <BDForm FormModel="@model" Components="@UserSettingsService.Components" Value="@data" ValueChanged="@update" OnSubmitted ="@Submit"></BDForm>
````

## Code

````csharp
        TestData data = new TestData() { name = "demo", birthDate = DateTime.Now.AddYears(-10) };

        private DynamicFormModel model = FormFactory.GroupLayout(gr => gr
                .WithParameter("style", "")
                ,
                 FormFactory.StringComponent("voornaam", "$.name", name => name
                       .WithMinimumLength(7)
                       .WithMaximumLength(14)
                       .WithInvalidMessage("Name should be at least {MinimumLength} and max {MaximumLength} long!")
                       ),
                  FormFactory.SubmitAction("Submit form - from the middle of the form"),
                  FormFactory.DateTimeComponent("Date", "$.birthDate", birthdate => birthdate
                       .WithFormat("dd-MM-YYYY")
                       ),
                  FormFactory.IntComponent("Age", "$.age"),
                  FormFactory.StringComponent("Address", "$.second.street")
               );


        public class TestData
        {
            public string name { get; set; }

            public int age { get; set; } 
            public DateTime birthDate { get; set; }
            public Address second { get; set; } = new Address();
        }

        public class Address
        {
            public string street { get; set; }
        }
````
