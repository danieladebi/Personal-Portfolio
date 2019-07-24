import tensorflow as tf
from collections import Counter
from data_processing import *
import numpy as np
from sklearn import model_selection, svm, neighbors
from sklearn.ensemble import VotingClassifier, RandomForestClassifier

def buy_sell_hold(*args):
    '''
    :param args: days
    :return: 1 if buy, 0 if hold, -1 if sell
    '''
    cols = [c for c in args]
    requirement = 0.02
    for col in cols:
        if col > requirement:
            return 1
        if col < -requirement:
            return -1
    return 0


def extract_featuresets(ticker):
    tickers, df, hm_days = process_data_for_labels(ticker)

    ticker_days = [df[f'{ticker}_{i+1}d'] for i in range(hm_days)]
    df[f'{ticker}_target'] = list(map(buy_sell_hold, *ticker_days))

    vals = df[f'{ticker}_target'].values.tolist()
    str_vals = [str(i) for i in vals]
    print("Data Spread:", Counter(str_vals))

    df.fillna(0, inplace=True)
    df = df.replace([np.inf, -np.inf], np.nan)
    df.dropna(inplace=True)

    df_vals = df[[ticker for ticker in tickers]].pct_change()
    df_vals = df_vals.replace([np.inf, -np.inf], 0)
    df_vals.fillna(0, inplace=True)

    X = df_vals.values
    y = df[f'{ticker}_target'].values

    return X, y, df


def train_and_run_model(ticker):
    X, y, df = extract_featuresets(ticker)

    X_train, X_test, y_train, y_test = model_selection.train_test_split(X,y,test_size=0.25)
    clf = VotingClassifier([('lsvc', svm.LinearSVC()),
                            ('knn', neighbors.KNeighborsClassifier()),
                            ('rfor', RandomForestClassifier())])

    clf.fit(X_train, y_train)
    confidence = clf.score(X_test, y_test)
    print('accuracy:', confidence)
    predictions = clf.predict(X_test)
    print('predicted class counts:', Counter(predictions))
    print()
    print()
    return confidence