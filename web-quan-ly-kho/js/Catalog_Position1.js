/// <reference name="MicrosoftAjax.js"/>
alert("hello");

function CallMe(Id, dest)

{
// call server side method

PageMethods.GetDate(Id, CallSuccess, CallFailed, dest);

}

// set the destination textbox value with the ContactName

function CallSuccess(res, destCtrl)
{
alert(res.toString());
}

// alert message on some failure

function CallFailed(res, destCtrl)

{

alert(res.get_message());

} 