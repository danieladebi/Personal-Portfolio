__NOTE THIS IS STILL A WORK IN PROGRESS! WILL CONTINUE UPDATING THIS PORTFOLIO. I WILL BE CONTINUOUSLY UPDATING THIS PAGE AS I CREATE NEW PROJECTS IN THE FUTURE.__

# Daniel's Personal Portfolio
## About Me and My Work
Hello! My name is Ikechukwu Daniel Adebi, and I am a member of MIT's class of 2022, majoring in Computer Science and Physics. This is my portfolio of projects I've worked on over the years. All of my work on this github spans from 2016, all the way up til today, but I have been programming since 2013, when I was in eighth grade. In this portfolio, however, I will only be describing my work from 2016 to present day. 

### List of Projects 

|Project Name | Language | Brief Description | Duration |
|:-----------:|:----------:|------------|:-----------:|
|[Quantum Machine Learning Model](#Quantum-Machine-Learning-Model)| Python | A support vector machine that runs on a virtual quantum system. This is my side project for IBM. | June 2019 - August 2019 |
|[Poker Hand Classifier](#Poker-Hand-Classifier) | Python | Neural network that classifies different poker hands.| July 2019 |
|[Stock Predictor](#Stock-Predictor) | Python | Machine Learning model that predicts stock prices.| June 2019 | 
|[SSA Game Engine](#SSA-Game-Engine) | Java | Game Engine I developed from scratch (w/ Tutorial Help). | January 2018 - April 2018 |
|[Chemistry Teaching Tool](#Chemistry-Teaching-Tool) | C# (Unity) | Chemistry Teaching Tool developed in Unity to teach high school students the fundamentals of chemistry. | January 2017 - May 2017 |
|[Surgery Learning Tool](#Surgery-Learning-Tool) | C# (KINECT) | Program that uses the KINECT camera to track students hand movements to teach them how to perform surgeries. | January 2017 - May 2017 |

## Projects 
### Quantum Machine Learning Model 
<p align="center">
  <img src="ibm.png" alt="IBM" width="200" />  

  <img src="circuit.png" alt="circuit" width="300" /> 
</p>

#### Description
This program is a quantum support vector machine that runs on IBM's QASM simulator, where QASM is a programming language for formally defining a quantum systems. This model takes in breast cancer data from the years 1958-1970 and determines whether or not someone lived more than 5 years after they have been diagnosed. This program uses the Qiskit library to run, and the survival date comes from UCI's machine learning repository.
#### Reason For Develepment
This project was an assignment I received during my time interning with IBM as a software developer. I explained to them that I had interest in learning more about how machine learning works, as well as my interest in physics (particularly quantum physics), and as a result, I got the opportunity to learn from people on the IBM Q team and work on this project.
#### Room For Improvement
The amount of data I used was very small (only a bit more than 300 datapoints), so my model was not very accurate (~70% accuracy was the best I achieved). Despite the amount of research I put into this project, I still had very little experience with quantum computing and almost no experience with machine learning going into this project, but I ended up learning a lot about both fields through this project so I'm pretty happy with my result.

[Back to Projects](#List-of-Projects)

### Poker Hand Classifier 
<p align="center"><img src="10h-9d-8s-6c-2h.png" alt="Poker Hand" width="400" /> </p>

#### Description 
This program is a neural network that is able to identify poker hands with over 99% accuracy (best model was 99.62% accurate). I used the UCI machine learning repository to gather data to train this model, and ended up using over 1,000,000 data points total to train and test the data. 
#### Reason For Development 
This is was the first time I created a neural network without following any tutorials online, and I was also curious to see how neural networks go about classifying different sets of numbers, and I found this poker hand classifier as an easy way to do so. 
#### Room For Improvement
While this model was generally very accurate, and accurate for more common hands (i.e. one pair, two pairs, etc.), this model was much less accurate for more rare hands like four of a kind, or a straight flush, or even just straights in general. This is most likely because the dataset I used was not evenly distributed, so if I were to make this project again, I would prolly try and get an evenly distributed amount of each hand type, so my neural network could classify the rarer hands more accurately. I'm still continuing the process of collecting data from this project to see what else can be improved upon.

[Back to Projects](#List-of-Projects)

### Stock Predictor 
#### Description
This is a machine learning model that takes in the prices of each stock in the S&P 500 and by using a support vector machine, predicts whether the prices will rise or fall and by how much. It can also determine whether or not you should buy, sell, or hold a stock for a certain timespan (default is 7 days) provided by the user. Credits to sentdex (youtube channel) and Yahoo Finance for providing me the resources and information I needed for this project.
#### Reason For Development 
Well, besides trying to get rich fast, I wanted to learn how to work with and process time-series data. While I don't have the strongest interest in finance, I do find stocks to be rather interesting, so I decided to see what I could do trying to predict them. 
#### Room For Improvement
This model was rather difficult to determine how accurate it was, considering that it decided to hold stock shares for the majority of the timeline. And when the model made the decision to buy or sell, it usually made the right choice, but there were occasions where it would miss major profits, and some stocks resulted in negative returns altogether. If I could remake this model, I would probably focus on a few stocks (or even just one) at a time, and train the model to maximize returns for those stocks only rather than trying to go for 500 at once.

[Back to Projects](#List-of-Projects)

### SSA Game Engine
#### Description

#### Reason For Develepment

#### Room For Improvement

[Back to Projects](#List-of-Projects)

### Chemistry Teaching Tool
#### Description

#### Reason For Develepment

#### Room For Improvement

[Back to Projects](#List-of-Projects)
