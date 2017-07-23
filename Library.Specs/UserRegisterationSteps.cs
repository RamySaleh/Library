using System;
using TechTalk.SpecFlow;

namespace Library.Specs
{
    [Binding]
    public class UserRegisterationSteps
    {
        [Given]
        public void GivenIAmOnTheRegisterationPage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void WhenIEnterMyData()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void WhenProceedWithUserCreation()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then]
        public void ThenTheUserIsCreatedAndRedirectedToTheCorrectPage()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
