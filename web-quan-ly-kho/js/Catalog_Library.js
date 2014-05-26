                /* <![CDATA[ */ 
                
                var gridId = "<%= RadTreeView1.ClientID %>";
                function isMouseOverGrid(target)
                {
                    parentNode = target;
                    while (parentNode != null)
                    {                    
                        if (parentNode.id == gridId)
                        {
                            return parentNode;
                        }
                        parentNode = parentNode.parentNode;
                    }
        
                    return null;
                }
        
                function onNodeDragging(sender, args)
                {
                    var target = args.get_htmlElement();    
                    
                    if(!target) return;
                    
                    if (target.tagName == "INPUT")
                    {        
                        target.style.cursor = "hand";
                    }

                    var grid = isMouseOverGrid(target)
                    if (grid)
                    {
                        grid.style.cursor = "hand";
                    }
                }
        
                function dropOnHtmlElement(args)
                {                    
                 if(droppedOnInput(args))
                 return;
                
                 if(droppedOnGrid(args))
                 return;                    
                }
                
                function droppedOnGrid(args)
                {
                 var target = args.get_htmlElement();
                
                 while(target)
                 {
                 if(target.id == gridId)
                 {
                 args.set_htmlElement(target);
                 return;                                                  
                 }
                
                 target = target.parentNode;
                 }
                 args.set_cancel(true);
                }
                
                function droppedOnInput(args)
                {
                 var target = args.get_htmlElement();
                    if (target.tagName == "INPUT")
                    {
                        target.style.cursor = "default";
                        target.value = args.get_sourceNode().get_text();        
                        args.set_cancel(true);
                        return true;
                    }                         
                }
        
                function dropOnTree(args)
                {
                    var text = "";

                    if(args.get_sourceNodes().length)
                    {    
                        var i;
                        for(i=0; i < args.get_sourceNodes().length; i++)
                        {
                            var node = args.get_sourceNodes()[i];
                            text = text + ', ' +node.get_text();
                        }
                    }
                }
                
                function clientSideEdit(sender, args)
                {
                 var destinationNode = args.get_destNode();                                 
                
                 if(destinationNode)
                 {            
                 var firstTreeView = $find('RadTreeView1');
                
                firstTreeView.trackChanges();
                var sourceNodes = args.get_sourceNodes();
                 for (var i = 0; i < sourceNodes.length; i++)
                 {
                            var sourceNode = sourceNodes[i];
                            sourceNode.get_parent().get_nodes().remove(sourceNode);                
                            
                            if(args.get_dropPosition() == "over")   destinationNode.get_nodes().add(sourceNode);                                                        
                            if(args.get_dropPosition() == "above")     insertBefore(destinationNode, sourceNode);
                            if(args.get_dropPosition() == "below")     insertAfter(destinationNode, sourceNode);
                 }
                 destinationNode.set_expanded(true);
                 firstTreeView.commitChanges();
                 }
                }
                
                function insertBefore(destinationNode, sourceNode)
                {
                 var destinationParent = destinationNode.get_parent();
                 var index = destinationParent.get_nodes().indexOf(destinationNode);
                 destinationParent.get_nodes().insert(index, sourceNode);
                }
                
                function insertAfter(destinationNode, sourceNode)
                {
                 var destinationParent = destinationNode.get_parent();
                 var index = destinationParent.get_nodes().indexOf(destinationNode);
                 destinationParent.get_nodes().insert(index+1, sourceNode);
                }                
                
                function onNodeDropping(sender, args)
                {            
                    var dest = args.get_destNode();
                    if (dest)
                    {
//                         var clientSide = document.getElementById('ChbClientSide').checked;
//        
//                         if(clientSide)
//                         {
                         clientSideEdit(sender, args);                
                         args.set_cancel(true);
                         return;
//                         }
                    
                        dropOnTree(args);
                    }
                    else
                    {
                        dropOnHtmlElement(args);
                    }
                }
                
                
                function onClientContextMenuShowing(sender, args)
                {
                    var treeNode = args.get_node();
                    treeNode.set_selected(true);
                    //enable/disable menu items
                    setMenuItemsState(args.get_menu().get_items(), treeNode);
                }
                
                function onClientContextMenuItemClicking(sender, args)
                {
                    var menuItem = args.get_menuItem();
                    var treeNode = args.get_node();
                    menuItem.get_menu().hide();
                    
                    switch(menuItem.get_value())
                    {
                        case "NewFolder":
                            break;
                        case "Delete":
                            var result = confirm("Bạn thật sự muốn xóa thư mục : " + treeNode.get_text()+"?");
                            args.set_cancel(!result);
                            break;                            
                    }
                }
                
                //this method disables the appropriate context menu items
                function setMenuItemsState(menuItems, treeNode)
                {
                    for (var i=0; i<menuItems.get_count(); i++)
                    {
                        var menuItem = menuItems.getItem(i);
                        switch(menuItem.get_value())
                        {
                            case "Delete":
                                formatMenuItem(menuItem, treeNode, 'Delete "{0}"');
                                break;                                
                            case "NewFolder":
                                if (treeNode.get_parent() == treeNode.get_treeView())
                                {
                                    menuItem.set_enabled(false);
                                }
                                else
                                {
                                    menuItem.set_enabled(true);
                                }
                                break;
                                
                        }
                    }
                }
                
                //formats the Text of the menu item
                function formatMenuItem(menuItem, treeNode, formatString)
                {
                    var nodeValue = treeNode.get_value();
                    if (nodeValue && nodeValue.indexOf("_Private_") == 0)
                    {
                        menuItem.set_enabled(false);
                    }
                    else
                    {
                        menuItem.set_enabled(true);
                    }
                    var newText = String.format(formatString, extractTitleWithoutMails(treeNode));
                    //menuItem.set_text(newText);
                }
            
                //checks if the text contains (digit)
                function hasNodeMails(treeNode)
                {
                    return treeNode.get_text().match(/\([\d]+\)/ig);
                }
                
                //removes the brackets with the numbers,e.g. Inbox (30)
                function extractTitleWithoutMails(treeNode)
                {
                    return treeNode.get_text().replace(/\s*\([\d]+\)\s*/ig, "");
                }       
                
                /* ]]> */