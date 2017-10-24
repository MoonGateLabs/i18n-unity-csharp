# i18n-unity-csharp

Lightweight internationalization for use with C#, uses common [`__('...')` gettext](https://www.gnu.org/software/gettext/manual/gettext.html) syntax.

Created and used by [Moon Gate Labs](http://moongatelabs.com/)

It allows developers to utilize multiple languages seamlessly within their Unity projects.

<del>It is also able to work outside of Unity projects as a standalone library!</del> See [#4](https://github.com/MoonGateLabs/i18n-unity-csharp/issues/4)

## Status

Beta - Version 0.9.4

## Platforms / Technologies

* [C#](http://en.wikipedia.org/wiki/C_Sharp_programming_language)
* [JSON](http://json.org/)
* [Unity](https://unity3d.com/)
* [Unity SimpleJSON](http://wiki.unity3d.com/index.php/SimpleJSON)

## Usage - Setup and configuration

1. Copy `I18nUnity` folder to your Unity3D project's `Assets` folder (or anywhere else Unity can see it will work)

2. Create your own `I18n` class (ex: in `Assets/Scripts`) that will _subclass_ `Mgl.I18n` so you can specify your own locales:

```csharp
// Replace MyApp with your own namespace
namespace MyApp {
  public class I18n : Mgl.I18n {
    protected static readonly I18n instance = new I18n();

    // Customize your languages here
    protected static string[] locales = new string[] {
      "en-US",
      "fr-FR",
      "es-ES",
      "de-DE"
    };

    public static I18n Instance {
      get {
        return instance;
      }
    }
  }
}
```

You can now use `I18n` by adding your namespace (in the following example, `MyApp`).

```csharp
using MyApp;
```

3. Create an instance of the class and be sure to only use it in methods after the `Start()` period occurs - do not call from within `Awake()`:

```csharp
private I18n i18n = I18n.Instance;

// ...

void Start()
{
    string hW = i18n.__("Hello World!");
}
```

4. Your translation files must be in JSON compliant format and be named according to their language and variant.

```
[project-root]
    |_ Assets
        |_ Resources
            |_ Locales
                en-GB.json
                en-US.json
                es-ES.json
                fr-FR.json
```

## Usage - Optional configuration

You can configure a few different settings using `Configure()`:

```csharp
I18n.Configure(
    string localePath = null       // Unity location for translations defaults to 'Locales' inside of 'Assets/Resources/Locales/'
    string defaultLocale = null    // language locale used, defaults to en-US
    bool logMissing = true         // log missing translations
);
```

You can change the path directly using the `SetPath()` function, although we recommend using the default path:

```csharp
I18n.SetPath("Locales/");
```

You can also change the locale at any time using the `SetLocale()` function:

```csharp
I18n.SetLocale("en-US");
```

## Usage - Translation JSON format

Some sample JSON:

```json
{
    "Hello": "Hello",
    "Hello {0}, how are you today?": "Hello {0}, how are you today?",
    "Combo: {0}x": "Combo {0}x",
    "You have one cat": {
        "zero": "You have no cats",
        "one": "You have one cat",
        "other": "You have a lot of cats!"
    },
    "You found {0} item": {
        "zero": "No items found",
        "one": "You found one item",
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
    "There is one monkey in the {1}": {
        "zero": "There are no monkeys in the {1}.",
        "one": "There is one monkey in the {1}.",
        "other": "There are {0} monkeys in the {1}!"
    }
}
```

## Usage - Examples and configuration

Here are some basic examples using the JSON above, you can also review the unit tests in `I18nTest` for more examples.

Assuming you are using Unity - *although this works without Unity* as well!

```csharp
Text test = null;
test.text = i18n.__("Hello");
// puts: Hello
test.text = i18n.__("Combo: {0}x", 5);
// puts: Combo: 5x
```

Zero, one, or 'other' amount (greater than 1 or less than -1)

```csharp
string message;
message = i18n.__("You have one cat", 0);
// puts: You have no cats
message = i18n.__("You have one cat", 1);
// puts: You have one cat
message = i18n.__("You have one cat", 45);
// puts: You have a lot of cats!
```

Replacements using zero, one, or 'other' amount (greater than 1 or less than -1)

```csharp
Text test = null;
test.text = i18n.__("{0} credits", 0);
// puts: No credits
test.text = i18n.__("{0} credits", 1);
// puts: 1 credit
test.text = i18n.__("{0} credits", 45);
// puts: 45 credits
test.text = i18n.__("{0} credits", 15.23);
// puts: 15.23 credits
test.text = i18n.__("{0} credits", 0.85);
// puts: 0.85 credits

test.text = i18n.__("You found {0} item", 0);
// puts: No items found
test.text = i18n.__("You found {0} item", 1);
// puts: You found one item
test.text = i18n.__("You found {0} item", 10);
// puts: You found 10 items

test.text = i18n.__("Score: {0} points", 0);
// puts: nada
test.text = i18n.__("Score: {0} points", 1);
// puts: Score: 1 point
test.text = i18n.__("Score: {0} points", 1000);
// puts: Score: 1000 points
test.text = i18n.__("Score: {0} points", -1);
// puts: Score: -1 point
test.text = i18n.__("Score: {0} points", -1000);
// puts: Score: -1000 points
```

String replacements

```csharp
string message;
message = i18n.__("Hello {0}, how are you today?", "Jane");
// puts: Hello Jane, how are you today?
message= i18n.__("Level {1} time: {0}", "00:30:29", "The Cave Level");
// puts: Level The Cave Level time: 00:30:29
```

String replacements and quantity checks

```csharp
System.Console.WriteLine( i18n.__("There is one monkey in the {1}", 0, "tree"));
// outputs: There are no monkeys in the tree.
System.Console.WriteLine(i18n.__("There is one monkey in the {1}", 1, "tree"));
// outputs: There is one monkey in the tree.
System.Console.WriteLine(i18n.__("There is one monkey in the {1}", 27, "tree"));
// outputs: There are 27 monkeys in the tree!
```

Change language

```csharp
System.Console.WriteLine(i18n.__("Hello"));
// outputs: Hello
I18n.SetLocale("fr-FR");
System.Console.WriteLine(i18n.__("Hello"));
// outputs: Bonjour
```

## License

[MIT](LICENSE.md) © [Cameron Manavian](https://github.com/camsjams)
