# Hackathon Submission Entry form

> __Important__  
> 
> Copy and paste the content of this file into README.md or face automatic __disqualification__  
> All headlines and subheadlines shall be retained if not noted otherwise.  
> Fill in text in each section as instructed and then delete the existing text, including this blockquote.

You can find a very good reference to Github flavoured markdown reference in [this cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet). If you want something a bit more WYSIWYG for editing then could use [StackEdit](https://stackedit.io/app) which provides a more user friendly interface for generating the Markdown code. Those of you who are [VS Code fans](https://code.visualstudio.com/docs/languages/markdown#_markdown-preview) can edit/preview directly in that interface too.

## Team name
Core Crafters

## Category
AI

## Description
⟹ Write a clear description of your hackathon entry.  

Module Purpose
This module automates content authoring in Sitecore XP by integrating OpenAI's API. It enhances efficiency, accuracy, and consistency by leveraging AI-generated PowerShell scripts and structured templates. Additionally, an AI-powered chatbot provides real-time assistance to users, improving their overall content creation experience.

Problem Solved
Manual content authoring in Sitecore XP can be time-consuming, error-prone, and inconsistent. Authors often struggle with maintaining structured templates and ensuring content quality.

How This Module Solves It
AI-Generated Templates: Automates the creation of structured templates based on user input, reducing manual effort.
AI-Powered Chatbot: Provides real-time guidance, automating repetitive tasks and assisting users within the Sitecore XP ecosystem.


## Video link
⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_

⟹ [Replace this Video link](#video-link)
https://youtu.be/fRu-yP5WshI?si=y0ae1mzprOQWtGfY

## Installation instructions
⟹ Write a short clear step-wise instruction on how to install your module.  

> - Install Sitecore Package files
> - Install the required package:
    ```sh
    dotnet add package OpenAI
    ```
> - Add your OpenAI API key in the code:
    ```csharp
    var apiKey = "YOUR_OPENAI_API_KEY";
    ```
	Ensure that your API key is securely stored and not hardcoded in production environments.

> - Install core template package for Templify AI button

> - Published all dlls and UI files.

for example:

1. Use the Sitecore Installation wizard to install the [package](#link-to-package)
2. ...
3. profit



## Usage instructions
⟹ Provide documentation about your module, how do the users use your module, where are things located, what do the icons mean, are there any secret shortcuts etc.

Include screenshots where necessary. You can add images to the `./images` folder and then link to them from your documentation:

![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")

You can embed images of different formats too:

![Deal With It](docs/images/deal-with-it.gif?raw=true "Deal With It")

And you can embed external images too:

![Random](https://thiscatdoesnotexist.com/)

## Comments
If you'd like to make additional comments that is important for your module entry.
