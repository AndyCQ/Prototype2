==================================================

Simple SplashScreen

Created by 'Tom OLIVIER'

==================================================

You have 2 example scenes for more understanding, do not hesitate to take a look.


Simple SplashScreen it's simple (:3)

You have 2 prefabs:
	-Manager (Your Splash Screens)
	-FadeOut (It's a magical prefab put on your next scene so that your splash screen fades to the next scene!)

How the managers work ?

Your splash screen is just a simple GameObject that you can customize without limit, (with unity tools, or yours, or those of others).
The splash screens scroll with either:
	-A length.
	-An animation.
	-A signal. (C# script)
During the scrolling your scene already loads and waits for the end to activate.
You can even just activate a script, for example, which loads the scene last, or a very special splash.

So the manager is the tool that allows you to create and display splash screens.