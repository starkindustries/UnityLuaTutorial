return function()
    print("Hello world WOOT WOOT!")
    SetText("I'm a lua script!")
    coroutine.yield()
    SetText("second slide")
end