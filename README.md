# i18n-unity-csharp
Lightweight internationalization for use with C#, uses common __('...') syntax.

Created and used by [Moon Gate Labs](http://moongatelabs.com/)

It allows developers to utilize multiple languages seamlessly within their Unity projects.

## Status
Beta - Version 0.9.1

## Platforms / Technologies
* [C#](http://en.wikipedia.org/wiki/C_Sharp_programming_language)
* [JSON](http://json.org/)
* [Unity](https://unity3d.com/)
* [Unity SimpleJSON](http://wiki.unity3d.com/index.php/SimpleJSON)

## Usage

Import `i18n-unity-csharp` into the class that you wish to utilize with:
    using Mgl.Locale;

Create an instance of the class and call `Configure()`:

    private I18n i18n = I18n.Instance;
    
    ...
    
    void Awake()
    {
            I18n.Configure();
    }

You can configure a few different settings using:

    I18n.Configure(
        string localePath = null       // Unity location for translations, defaults to 'Assets/Resources/Locales/'
        string defaultLocale = null    // language locale used, defaults to en-US
        bool logMissing = true         // log missing translations to Debug.Log
    );

Some sample JSON:

    {
        "Hello": "Hello",
        "Hello %s, how are you today?": "Hello %s, how are you today?",
        "Combo: {0}x": "Combo {0}x",
        "You found {0} item": {
            "zero": "No items found",
            "one": "You found {0} item",
            "other": "You found {0} items"
        },
        "{0} credits": {
            "zero": "No credits",
            "one": "{0} credit",
            "other": "{0} credits"
        },
        "Score: {0} points": {
            "zero": "nada",
            "one": "Score: {0} point",
            "other": "Score: {0} point"
        },
        "Level {1} time: {0}": "Level {1} time: {0}",
        "Hit": {
            "zero": "hit!",
            "one": "-{0}",
            "other": "-{0}"
        },
        "There is one monkey in the %s": {
            "one": "There is one monkey in the %s",
            "other": "There are %d monkeys in the %s"
        }
    }

Here are some basic examples using the JSON above (more coming soon along with tests):

    Text test = null;
    test.text = i18n.__("Hello",);
    test.text = i18n.__("Combo: {0}x", 5);
    test.text = i18n.__("{0} credits", 0);
    test.text = i18n.__("{0} credits", 1);
    test.text = i18n.__("{0} credits", 45);
    test.text = i18n.__("You found {0} item", 0);
    test.text = i18n.__("You found {0} item", 1);
    test.text = i18n.__("You found {0} item", 10);
    test.text = i18n.__("Score: {0} points", 0);
    test.text = i18n.__("Score: {0} points", 1);
    test.text = i18n.__("Score: {0} points", 1000);
    test.text = i18n.__("Level {1} time: {0}", '00:30:29", "The Cave Level");