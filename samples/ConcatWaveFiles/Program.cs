using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libwave.net;
using libwave.net.Wave;

namespace ConcatWaveFiles
{
	class Program
	{
		static void Main(string[] args)
		{
			////Method one
			//string fileOne=args[0];
			//string fileTwo=args[1];

			//WaveFileReader waveOne=new WaveFileReader(fileOne);
			//WaveFileReader waveTwo=new WaveFileReader(fileTwo);

			//WaveFormat waveFormat=new WaveFormat(44100, 2); //all in-files must be have this samplerate
			//WaveFileWriter writer=new WaveFileWriter("output.wav", waveFormat);
			
			//byte[] waveOneBytes=new byte[waveOne.Length];
			//waveOne.Read(waveOneBytes, 0, waveOneBytes.Length);

			//byte[] waveTwoBytes=new byte[waveTwo.Length];
			//waveTwo.Read(waveTwoBytes, 0, waveTwoBytes.Length);

			//writer.Write(waveOneBytes, 0, waveOneBytes.Length);
			//writer.Write(waveTwoBytes, 0, waveTwoBytes.Length);

			//writer.Close();

			//Method two
			string fileOne=args[0];
			string fileTwo=args[1];

			WaveFileReader waveOne=new WaveFileReader(fileOne);
			WaveFileReader waveTwo=new WaveFileReader(fileTwo);

			WaveFormat waveFormat=new WaveFormat(44100, 2); //all in-files must be have this samplerate
			WaveFileWriter writer=new WaveFileWriter("output.wav", waveFormat);

			Write(waveOne, writer);
			Write(waveTwo, writer);

			writer.Close();
		}

		static void Write(WaveFileReader reader, WaveFileWriter writer)
		{
			float[] sampleFrame=null;

			do
			{
				sampleFrame=reader.ReadNextSampleFrame();

				if(sampleFrame!=null)
				{
					if(sampleFrame.Length==1) //Only mono
					{
						float mono=sampleFrame[0];
						sampleFrame=new float[2];
						sampleFrame[0]=mono;
						sampleFrame[1]=mono;
					}

					//TODO resampling needed if different rates
					writer.WriteSamples(sampleFrame, 0, 2);
				}
			}
			while(sampleFrame!=null);
		}
	}
}
