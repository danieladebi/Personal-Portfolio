# Daniel's Personal Portfolio
## About Me and My Work
Hello! My name is Ikechukwu Daniel Adebi, although I personally go by Daniel, and I am a member of MIT's class of 2022, majoring in Computer Science and Physics. This is my portfolio of projects I've worked on over the years. All of my work on this portfolio spans from 2016, all the way up to today, but I have been programming since 2013, when I was in eighth grade. In this portfolio, however, I will only be describing my work from 2016 to present day.

This page will be updated as I continue to create more projects. Enjoy!

### List of Projects 

|Project Name | Language | Brief Description | Duration |
|:-----------:|:----------:|------------|:-----------:|
|[Quantum Machine Learning Model](#Quantum-Machine-Learning-Model)| Python | A support vector machine that runs on a virtual quantum system. This was my side project for my internship at IBM. | June 2019 - August 2019 |
|[Poker Hand Classifier](#Poker-Hand-Classifier) | Python | Neural network that classifies different poker hands.| July 2019 |
|[Stock Predictor](#Stock-Predictor) | Python | Machine Learning model that predicts stock prices.| June 2019 | 
|[SSA Game Engine](#SSA-Game-Engine) | Java | 3D Game Engine I developed from scratch for my Independent Study in high school. (SSA = Simulation Software Analysis) | January 2018 - April 2018 |
|[Chemistry Learning Tool](#Chemistry-Learning-Tool) | C# (Unity) | Chemistry Learning Tool developed in Unity to teach high school students the fundamentals of chemistry. | January 2017 - May 2017 |
|[Surgery Learning Tool](#Surgery-Learning-Tool) | C# (Kinect) | Program that uses the Kinect camera to track students hand movements to teach them how to perform surgeries. | January 2017 - May 2017 |
|[Expedition of the Cosmos](#Expedition-of-the-Cosmos) | C# (XNA) | Video game that simulates motion from all planets from Earth to Neptune. Motion data modeled using Wolfram Mathematica. | September 2016 - December 2016 |

## Projects 
### Quantum Machine Learning Model 
<br />
<p align="center">
  <img src="other_materials/ibm.png" alt="IBM" width="250" />  
</p>

#### Results/Output:
```python
X train size: 244
X test size: 62
Y train size: 244
Y test size: 62
testing success ratio: 0.7940645161290323
```

#### Description
This program is a quantum support vector machine that runs on IBM's QASM simulator, where QASM is a programming language for formally defining a quantum systems. This model takes in breast cancer data from the years 1958-1970 and determines whether or not someone lived more than 5 years after they have been diagnosed. This program uses the Qiskit library to run, and the survival date comes from UCI's machine learning repository.
#### Reason For Develepment
I received this project as an assignment during my time interning with IBM as a software developer. I explained to them that I had interest in learning more about how machine learning works, as well as my interest in physics (particularly quantum physics), and as a result, I got the opportunity to learn from people on the IBM Q team and work on this project.
#### Room For Improvement
The amount of data I used was very small (only a bit more than 300 datapoints), so my model was not very accurate (~70% accuracy was the best I achieved). Despite the amount of research I put into this project, I still had very little experience with quantum computing and almost no experience with machine learning going into this project, but I ended up learning a lot about both fields through this project so I'm pretty happy with my result.

[Back to Projects](#List-of-Projects)

### Poker Hand Classifier 
<p align="center"><img src="other_materials/10h-9d-8s-6c-2h.png" alt="Poker Hand" width="400" /> </p>

#### Results/Output
```python

...
        S1  C1  S2  C2  S3  C3  S4  C4  S5  C5
24586   1   4   3   3   4   4   1   3   2   4
Prediction: False | Predicted: Straight | Actual: Nothing | Hand 
        S1  C1  S2  C2  S3  C3  S4  C4  S5  C5
24644   2  10   2  13   4  12   3  11   1   3
Prediction: True | Predicted: Full House | Actual: Full House | Hand 
        S1  C1  S2  C2  S3  C3  S4  C4  S5  C5
24665   1  10   3   3   3  10   2  10   4   3
Prediction: True | Predicted: Full House | Actual: Full House | Hand 
        S1  C1  S2  C2  S3  C3  S4  C4  S5  C5
24776   1   6   2   6   4  12   3   6   1  12
Prediction: True | Predicted: Full House | Actual: Full House | Hand 
        S1  C1  S2  C2  S3  C3  S4  C4  S5  C5
24977   3   4   1   4   2   4   2   6   3   6

Accuracy on test data: 0.99671
Accuracy on predicted data: 0.996481407437501
```

#### Description 
This program is a neural network that is able to identify poker hands with over 99% accuracy (best model was 99.67% accurate). I used the UCI machine learning repository to gather data to train this model, and ended up using over 1,000,000 data points in total to train and test the data (800,000 to train, and 200,000 to test). I also used this model to predict an extra 25,000 hands to make sure it was accurate with unfamiliar input. Below I describe how the data is represented in the output above:

- "#" corresponds to which specific card is being referred to in a given hand. (Ranges from 1-5)
- "S#" represents what suit the card is. (1 => Hearts, 2 => Spades, 3 => Diamonds, 4 => Clubs)
- "C#" represents what rank the card is. (1 => Aces, 11 => Jacks, 12 => Queens, 13 => Kings, All other numbers are normal)

#### Reason For Development 
This is was the first time I created a neural network without following any tutorials online, and I was also curious to see how neural networks go about classifying different sets of numbers, and I found this poker hand classifier as an easy way to do so. 
#### Room For Improvement
While this model was generally very accurate, and accurate for more common hands (i.e. one pair, two pairs, etc.), This is most likely because the dataset I used was not evenly distributed, so if I were to make this project again, I would prolly try and get an evenly distributed amount of each hand type, so my neural network could classify the rarer hands more accurately. 

For example, flushes were virtually impossible for my program to detect because out of the 1,000,000 hands that the model processed, only about 2000 of these hands were actually some form of flush, so as a result, it was virtually impossible for this model to classify flushes, straight flushes, or royal flushes. 

Below is a data and accuracy distribution so you get a clearer picture of what I'm describing.
```
Count per hand (for 1,000,000 hands):
{"Nothing 0": 501209, "One Pair 1": 422498, "Two Pairs 2": 47622, "Three of a Kind 3": 21121, "Straight 4": 3885, "Flush 5": 1996, "Full House 6": 1424, "Four of a Kind 7": 230, "Straight Flush 8": 12, "Royal Flush 9": 3}

Model Accuracy Per Hand:
{0: 0.9993596413990358,
 1: 0.9998113029529495,
 2: 0.9975124378109744,
 3: 1.0000000000000142,
 4: 0.9032258064516123,
 5: 0,
 6: 0.9444444444444448,
 7: 0.9999999999999999,
 8: 0,
 9: 0}
```
<p align = "center"><img src="Poker Hand Classifier/poker_hand_distribution.PNG" width="800" /></p>
<p align = "center"><img src="Poker Hand Classifier/acc_per_hand.PNG" width="800" /></p>

[Back to Projects](#List-of-Projects)

### Stock Predictor 
<p align="center">
  <img src="other_materials/yahoo_finance.png" alt="YF" width=195" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <img src="other_materials/stocks.PNG" alt="Stocks" width="500" /> 
</p>

#### Description
This is a machine learning model that takes in the prices of each stock in the S&P 500 and by using a voting classifier, predicts whether the prices will rise or fall and by how much. It can also determine whether or not you should buy, sell, or hold a stock for a certain timespan (default is 7 days) provided by the user. Credits to sentdex (youtube channel) and Yahoo Finance for providing me the resources and information I needed for this project.
#### Reason For Development 
Well, besides trying to get rich fast, I wanted to learn how to work with and process time-series data. While I don't have the strongest interest in finance, I do find stocks to be rather interesting, so I decided to see what I could do trying to predict them. 
#### Room For Improvement
This model was rather difficult to determine how accurate it was, considering that it decided to hold stock shares for the majority of the timeline. And when the model made the decision to buy or sell, it usually made the right choice, but there were occasions where it would miss major profits, and some stocks resulted in negative returns altogether. If I could remake this model, I would probably focus on a few stocks (or even just one) at a time, and train the model to maximize returns for those stocks only rather than trying to go for 500 at once.

[Back to Projects](#List-of-Projects)

### SSA Game Engine
<p align="center"> <img src="other_materials/SSA2.gif" alt="SSA2" width="250" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <img src="other_materials/SSA.gif" alt="SSA" width="300" />  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <img src="other_materials/SSA3.gif" alt="SSA3" width="250" /></p>

#### Description
I developed a 3D game engine for an independent study I did in my senior year of high school. I used various youtube tutorials to help me along the way. The engine is capable of simulating real world physics and is also highly customizable. 
#### Reason For Develepment
The independent study I did for this project was called "Simulation Software Analysis", or SSA, because for the entire year, I studied different types of simulation software. I decided to make a game engine in particular for the final part of this independent study because at the time, most of my programming experience and interest came from video games, and I wanted to learn more about the environments I was developing in. 
#### Room For Improvement
I feel that I could have made the engine more intuitive and simpler to use, because as of right now, while I feel that I did a pretty good job at structuring and organizing my code, I may have condensed processes like rendering to a point where there isn't much flexibility or freedom to change much about its process.

[Back to Projects](#List-of-Projects)

### Chemistry Learning Tool
<p align="center"> <img src="other_materials/CLT.png" alt="CLT" width="350" /> </p> 
<p align="center"> <img src="other_materials/CLT2.gif" alt="CLT2" width="400" />  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <img src="other_materials/CLT3.gif" alt="CLT3" width="400" /></p>

#### Description
The Chemistry Learning Tool is a program I developed to teach high school students introductory level chemistry. It covers topics like atomic structure, periodicity, bonding, and others. This tool helped sophomores and juniors in my grade study for their chemistry exams.
#### Reason For Develepment
In high school, I found chemistry to be a pretty easy subject for me to learn. But I found that many of my classmates struggled in class for various reasons, whether it was because they weren't able to remember all of the correct electron configurations, or if they just could not figure out how different atoms bonded with each other. So I developed this program to help my classmates and friends better understand the material. 
#### Room For Improvement
A lot of the information in this program is just text listed in paragraph form, rather than in clearer and more concise bullet points or sections. Shorter text would also allow for students to better absorb the information they are provided, rather than being forced to read through texts that's similar to the textbooks. I also was not able to cover all topics covered in our Chemistry classes, so I could have covered more specific topics like different kinds of chemical reactions, and displayed animations or visuals of these concepts. 

[Back to Projects](#List-of-Projects)

### Surgery Learning Tool
<p align="center"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <img src="other_materials/surg1.png" alt="surg1" width="50" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <img src="other_materials/surg2.png" alt="surg2" width="300" />  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <img src="other_materials/surg3.png" alt="surg3" width="225" /></p>

__Demonstration__: (NOTE: video does not display actual project, but it does display my demonstration of the project to a certified orthopaedic surgeon, Dr. William Mackenzie of Nemours A.I. Dupont Children's Hospital in Wilmington, Delaware.)

[Link to Demonstration/Presentation](https://drive.google.com/open?id=0B90WXoM2aiWeOTBLb0N3UkFQR2c)

#### Description
The Surgery Learning Tool is a program designed for the Kinect console to teach students how to perform different kinds of surgery. This program teaches you how to perform diffferent types of surgeries, step by step. There are three types of surgery this program focuses on, and these are orthopedic, plastic, and cancer removal surgeries. 

#### Reason For Develepment
I developed this program for my computer science project in my junior year of high school. We had to develop a program that was able to teach other people how to do certain tasks through the use of human movement (hence the reason I used the Kinect console). At this time, I was also interested in learning more about the medical field, and more specifically, how surgeries are performed. So I decided to spend time to do some research with these topics and developed this program.

#### Room For Improvement
There were challenges I faced working with the limitations of the Kinect console itself, and how well it could detect the user's hands while they're performing the surgery. When the user had to perform specific cuts, there were times I was unable to detect the user accurately enough, as the Kinect camera we were provided could only detect the user's hands and not anything more specific like their fingers. 

[Back to Projects](#List-of-Projects)

### Expedition of the Cosmos
<p align="center"> <img src="other_materials/eotc1.gif" alt="EotC1" width="350" /> </p> 
<p align="center"> <img src="other_materials/EotC2.gif" alt="EotC2" width="300" />  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <img src="other_materials/EotC3.gif" alt="EotC3" width="300" /></p>

#### Description
Expedition of the Cosmos is a 3D space simulation game, where the objective is to exit the solar system from Earth by only using asteroids as a fuel source. This game is a simulation because it models the planetary movements of the solar system in real life (with accurate time scale as well), and I gathered data to model the planetary orbits using Wolfram Mathematica. All 3D models were also made by me using Autodesk Maya.

#### Reason For Develepment
I have always been, and still am, interested in space and astronomy, so I wanted to make a game based off of these topics. This project was also the first complete game I developed from scratch, so I wanted to see how far I could push my programming skills with my knowledge I had at the time, and I thought a fun way to do this was through video games.

#### Room For Improvement
The functions I developed to model the planetary movements were based off of their locations away from Earth (since the player starts from this point), rather than the Sun, which most likely resulted in a less accurate depiction on how the planets actually moved. Also for a good majority of gameplay, the game itself is rather boring, as you are really just traveling through space with not much to avoid or attack, so while I did try to focus on achieving realism in this project, I feel that I still could have at least made the game a little bit more fun. 

[Back to Projects](#List-of-Projects)

