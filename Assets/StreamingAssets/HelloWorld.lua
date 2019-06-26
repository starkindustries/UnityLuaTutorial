return function()
    print("Hello "..State.PlayerName.."! WOOT WOOT!")
    SetText("Hello "..State.PlayerName.."! It's working!")
    coroutine.yield()
    SetText("second slide")
end