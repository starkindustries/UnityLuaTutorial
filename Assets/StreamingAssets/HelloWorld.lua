-- HelloBranch function
local helloBranch = function() 
    SetText("Are you still here? [3]") 
    State.SetFlag("FirstButton", true)

    coroutine.yield()

    SetText("Hmm, what do you want now? [4]")
end

-- Main function
local main = function()
    -- Display a message to the debug console
    print("Hello "..State.PlayerName.."! WOOT WOOT!")
    
    -- Display a message in the dialogue box
    SetText("Hello "..State.PlayerName.."! Choose an option! [1]")

    -- Set the button's UI text
    ShowButtons("Lambo", "Tesla")
    
    -- wait for user input
    coroutine.yield()

    if State.ButtonSelected == 1 then
        SetText("You chose the lambo. Typical.. [SPACEBAR] [2]")
        -- Note that HelloBranch function must be declared before using it.
        -- This is just how Lua works.
        coroutine.yield(helloBranch)
    else     
        SetText("You chose the TESLA! EXCELLENT CHOICE! [SPACEBAR]")
        State.SetFlag("SecondButton", true)
        coroutine.yield()
    end    

    if State.GetFlag("FirstButton") then
        SetText("FirstButton flag was set! Lambo chosen!")
    elseif State.GetFlag("SecondButton") then
        SetText("SecondButton flag was set! Tesla chosen!")
    end
end

return main