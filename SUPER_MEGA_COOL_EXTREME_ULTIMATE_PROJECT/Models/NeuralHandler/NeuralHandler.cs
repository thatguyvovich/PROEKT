using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CyberPushkin.addirionDLL;
using CyberPushkin.observer;
using CyberPushkin.model;
using CyberPushkin.ngram;
using Encog.Neural.Networks ;
using Encog.Neural.Networks.Layers;
using Encog.Engine.Network.Activation;
using Encog.ML.Data ;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Neural.Networks.Training.Propagation;
using Encog.ML.Train ;
using Encog.ML.Data.Basic;
using Encog;
using Encog.Persist;

namespace CyberPushkin.NeuralHandler
{
    public class NeuralHandler : Observable
    {

        private readonly Position positon;
        private readonly int inputLayerSize;
        private readonly int outputLayerSize;
        private readonly BasicNetwork network;
        private readonly List<VocabularyWord> vocabulary;
        private readonly NGramStrategy nGramStrategy;
        private readonly List<Observer> observers = new List<Observer>();

        public NeuralHandler(String PartOftrainedNetwork, Position positon, List<VocabularyWord> vocabulary, NGramStrategy nGramStrategy)
        {
            if (positon == null ||
                positon.getName().Equals("") ||
                positon.getPossibleValues() == null ||
                positon.getPossibleValues().Count == 0 ||
                vocabulary == null ||
                vocabulary.Count == 0 ||
                nGramStrategy == null)
            {
                throw new ArgumentException();
            }

            this.positon = positon;
            this.vocabulary = vocabulary;
            this.inputLayerSize = vocabulary.Count;
            this.outputLayerSize = positon.getPossibleValues().Count;
            this.nGramStrategy = nGramStrategy;

            if (PartOftrainedNetwork == null)
            {
                this.network = createNeuralNetwork();
            }
            else
            {
                // load neural network from file
                try
                {                
                    this.network = (BasicNetwork) EncogDirectoryPersistence.LoadObject(new FileInfo(PartOftrainedNetwork));
                }
                catch (PersistError e)
                {
                    throw new ArgumentException();
                }
            }
        }

        public NeuralHandler(Position positon, List<VocabularyWord> vocabulary, NGramStrategy nGramStrategy) : this(null, positon, vocabulary, nGramStrategy)
        {
        }

        public static void shutdown()
        {
            EncogFramework.Instance.Shutdown();
        }

        private BasicNetwork createNeuralNetwork()
        {
            BasicNetwork network = new BasicNetwork();

            // input layer
            network.AddLayer(new BasicLayer(null, true, inputLayerSize));

            // hidden layer
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, inputLayerSize / 6));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, inputLayerSize / 6 / 4));

            // output layer
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, outputLayerSize));

            network.Structure.FinalizeStructure();
          //  network.GetStructure().readonlyizeStructure();
            network.Reset();

            return network;
        }

        public PositionValue classify(Verse verse)
        {
            double[] output = new double[outputLayerSize];

            // calculate output vector
            network.Compute(getTextAsVectorOfWords(verse), output);
            EncogFramework.Instance.Shutdown();

            return convertVectorToCharacteristic(output);
        }

        private PositionValue convertVectorToCharacteristic(double[] vector)
        {
            int idOfMaxValue = getIdOfMaxValue(vector);

            // find CharacteristicValue with found Id
            //

            foreach (PositionValue pv in positon.getPossibleValues())
            {
                if (pv.getId() == idOfMaxValue)
                {
                    return pv;
                }
            }

            return null;
        }

        private int getIdOfMaxValue(double[] vector)
        {
            int indexOfMaxValue = 0;
            double maxValue = vector[0];

            for (int i = 1; i < vector.Length; i++)
            {
                if (vector[i] > maxValue)
                {
                    maxValue = vector[i];
                    indexOfMaxValue = i;
                }
            }

            return indexOfMaxValue + 1;
        }

        public void saveTrainedClassifier(String PartOftrainedNetwork)
        {
            EncogDirectoryPersistence.SaveObject(new System.IO.FileInfo(PartOftrainedNetwork), network);// saveObject(trainedNetwork, network);
            notifyObservers("Trained Classifier for '" + positon.getName() + "' positon saved. Wait...");
        }

        public Position getCharacteristic()
        {
            return positon;
        }

        public void train(List<Verse> verse)
        {
            // prepare input and ideal vectors
            // input <- ClassifiableText text vector
            // ideal <- positonValue vector
            //

            double[][] input = getInput(verse);
            double[][] ideal = getIdeal(verse);

            // train
            //

            Propagation train = new ResilientPropagation(network, new BasicMLDataSet(input, ideal));
            train.ThreadCount = 16;

            do
            {
                train.Iteration();
                notifyObservers("Training Classifier for '" + positon.getName() + "' positon. Errors: " + String.Format("%.2f", train.Error * 100) + "%. Wait...");
            } while (train.Error > 0.01);

            train.FinishTraining();
            notifyObservers("Classifier for '" + positon.getName() + "' positon trained. Wait...");
        }

        private double[][] getInput(List<Verse> verses)
        {
            double[][] input =  new double[verses.Count][];

            // convert all classifiable texts to vectors
            //

            int i = 0;

            foreach (Verse verse in verses)                 
            {               
                input[i++] = getTextAsVectorOfWords(verse);
            }

            return input;
        }

        private double[][] getIdeal(List<Verse> verses)
        {
            double[][] ideal = new double[verses.Count][];

            // convert all classifiable text positons to vectors
            //

            int i = 0;

            foreach (Verse verse in verses)
            {
                ideal[i++] = getCharacteristicAsVector(verse);
            }

            return ideal;
        }

        // example:
        // count = 5; id = 4;
        // vector = {0, 0, 0, 1, 0}
        private double[] getCharacteristicAsVector(Verse verse)
        {
            double[] vector = new double[outputLayerSize];
            vector[verse.getCharacteristicValue(positon).getId() - 1] = 1;
            return vector;
        }

        private double[] getTextAsVectorOfWords(Verse verse)
        {
            double[] vector = new double[inputLayerSize];

            // convert text to nGramStrategy
            ISet<String> uniqueValues = nGramStrategy.getNGram(verse.getText());

            // create vector
            //

            foreach (String word in uniqueValues)
            {
                VocabularyWord vw = findWordInVocabulary(word);

                if (vw != null)
                { // word found in vocabulary
                    vector[vw.getId() - 1] = 1;
                }
            }

            return vector;
        }

        private VocabularyWord findWordInVocabulary(String word)
        {
            try
            {
                return vocabulary[vocabulary.IndexOf(new VocabularyWord(word))];
            }
            catch (Exception e) {
                if (e is NullReferenceException || e is IndexOutOfRangeException)
                {
                    return null;
                }
                else
                {
                    throw e;
                    //?????
                }

            }
            }

          
  public String toString()
        {
            return positon.getName() + "NeuralNetworkClassifier";
        }

       
  public void addObserver(Observer o)
        {
            observers.Add(o);
        }

        
  public void removeObserver(Observer o)
        {
            observers.Remove(o);
        }
  public void notifyObservers(String text)
        {
            foreach (Observer o in observers)
            {
                o.update(text);
            }
        }



    }
}
