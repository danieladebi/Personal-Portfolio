import pandas as pd
from sklearn import model_selection
import tensorflow as tf
from tensorflow import keras
import numpy as np
import pickle

from sklearn import linear_model, preprocessing

suits = {1:"Hearts", 2:"Spades", 3:"Diamonds", 4:"Clubs"}
ranks = {1:"Ace", 11:"Jack", 12:"Queen", 13:"King"}
hands = {0:"Nothing",
         1:"One Pair",
         2:"Two Pairs",
         3:"Three of a Kind",
         4:"Straight",
         5:"Flush",
         6:"Full House",
         7:"Four of a Kind",
         8:"Straight Flush",
         9:"Royal Flush"}

for i in range(2,11):
    ranks[i] = i

poker_data = pd.read_csv("poker-hand-training-true.data")
test_poker_data = pd.read_csv("poker-hand-testing.data",names=["S1","C1",'S2','C2','S3','C3','S4','C4','S5','C5','CLASS'])
predict = "CLASS"

from collections import Counter

print(Counter(poker_data[predict]))
print(Counter(test_poker_data[predict]))

X = test_poker_data.drop([predict], 1)
y = test_poker_data[predict]

X_train, X_test, y_train, y_test = model_selection.train_test_split(X,y, test_size=0.2)


def create_model():
    model = keras.Sequential([
        keras.layers.Dense(10, activation=tf.nn.relu),
        keras.layers.Dense(512, activation=tf.nn.relu),
        keras.layers.Dense(512, activation=tf.nn.relu),
        keras.layers.Dense(10, activation=tf.nn.softmax) # probability
    ])

    model.compile(optimizer="adam",
                  loss="sparse_categorical_crossentropy",
                  metrics=["accuracy"])
    model.fit(X_train.values, y_train.values, epochs=5) # or 5 but that may be too long
    model.save("poker_model.h5")
    return model


model = keras.models.load_model("poker_model.h5")
#model.summary()

test_loss, acc = model.evaluate(X_test.values, y_test.values)


def predict_model(model, X_test, y_test):
    predicted = model.predict(X_test)
    model_acc = 0
    for p, x, y in zip(predicted, range(len(X_test)), y_test):
        if np.argmax(p) > 3:
            print("Prediction:", np.argmax(p)==y, "| Predicted:", hands[np.argmax(p)], "| Actual:", hands[y], "| Hand \n", X_test[x:x+1])
        if np.argmax(p) == y:
            model_acc += 1/len(X_test)
    print("Accuracy on predicted data:", model_acc)


predict_model(model, poker_data.drop(["CLASS"], 1), poker_data["CLASS"])
#model.predict(poker_data.drop(["CLASS"],1)))

print("Accuracy on test data:", acc)
