using System;
using libwave.net;
using libwave.net.Wave;

namespace WriteWaveFile
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            WaveFileWriter writer=new WaveFileWriter("output.wav", new WaveFormat(44100, 2));
            WriteSilence(writer, 5);
            WriteSignal(writer, 5);
            WriteSilence(writer, 5);
            writer.Close();
        }

        static void WriteSilence(WaveFileWriter writer, double seconds)
        {
            float[] sampleFrame=new float[2];
            sampleFrame[0]=0;
            sampleFrame[1]=0;

            int count=(int)(5525*seconds);
            for(int i=0; i<count; i++)
            {
                writer.WriteSamples(sampleFrame, 0, 2);
            }
        }

        static void WriteSignal(WaveFileWriter writer, double seconds)
        {
            float amplitude=0.25f;
            float frequency=1000;

            float[] sampleFrame=new float[2];

            int count=(int)(5525*seconds);
            for(int i=0; i<count; i++)
            {
                float sample=(float)(amplitude*Math.Sin((2*Math.PI*i*frequency)/writer.WaveFormat.SampleRate));
                sampleFrame[0]=sample;
                sampleFrame[1]=sample;

                writer.WriteSamples(sampleFrame, 0, 2);
            }
        }
    }
}
