   function ScrollToSelectedNode()
             {
                //* get selected node id
             //* 'tvwScrollTo' - is our tree view id
               var selectedNodeID = $('#<%=trMenu.ClientID %>_SelectedNode').val();
   
                if (selectedNodeID != '')
               {
                    //* calculate selected top an left position (http://docs.jquery.com/CSS/position)
                   //* in order to get correct relative position remember to set div position to absolute
                  var scrollTop = $('#' + selectedNodeID).position().top;
                var scrollLeft = $('#' + selectedNodeID).position().left;
                  
                  //* here 'divTreeViewScrollTo' is the id of the div where we put our tree view
                    $('#divTreeViewScrollTo').scrollTop(scrollTop);
                  $('#divTreeViewScrollTo').scrollLeft(scrollLeft);
              }
         }