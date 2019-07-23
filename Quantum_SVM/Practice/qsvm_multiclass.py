from Practice.datasets import *
from qiskit import BasicAer
from qiskit.aqua.input import ClassificationInput
from qiskit.aqua import run_algorithm

n = 2 # Dimension of each data point
sample_Total, training_input, test_input, class_labels = Digits(
    training_size=40,
    test_size=10,
    n=n,
    PLOT_DATA=True
)
temp = [test_input[k] for k in test_input]
total_array = np.concatenate(temp)

aqua_dict = {
    'problem': {'name': 'classification', 'random_seed':10598},
    'algorithm': {
        'name': 'QSVM'
    },
    'feature_map': {'name': 'SecondOrderExpansion', 'depth':2, 'entangler_map': [[0,1]]},
    'multiclass_extension': {'name': 'AllPairs'},
    'backend': {'shots': 1024} #1024
}
backend = BasicAer.get_backend('qasm_simulator')
algo_input = ClassificationInput(training_input, test_input, total_array)
result = run_algorithm(aqua_dict, algo_input, backend=backend)
for k,v in result.items():
    print(f"'{k}' : {v}")