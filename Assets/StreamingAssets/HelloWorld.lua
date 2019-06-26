return function()
    -- Display a message to the debug console
    print("Hello "..State.PlayerName.."! WOOT WOOT!")
    
    -- Display a message in the dialogue box
    SetText("Hello "..State.PlayerName.."! Choose an option!")

    -- Set the button's UI text
    ShowButtons("Lambo", "Tesla")
    
    -- wait for user input
    coroutine.yield()

    if State.ButtonSelected == 1 then
        SetText("You chose the lambo. Typical..")
    else     
        SetText("You chose the TESLA! EXCELLENT CHOICE!")
    end
end