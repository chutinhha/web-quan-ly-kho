function ViewData(Id,Option)
    {
    
        var response;
        Position_Detail.ViewData(Id,Option,GetData_CallBack);  
    }
    
    function GetData_CallBack(response)
    {
        var response=response.value;
        
        if(response=="Empty")
        {
            alert("No Record Found !!!");
        }
        else if(response=='Error')
        {
            alert("An Error occured in accessing the DataBase !!!");
        }
    }
    
