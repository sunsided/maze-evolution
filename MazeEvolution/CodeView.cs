using System;
using System.Text;
using System.Windows.Forms;
using Evolution;
using System.Linq;

namespace MazeEvolution
{
	/// <summary>
	/// Zeigt den Code an
	/// </summary>
	/// <remarks></remarks>
	public partial class CodeView : Form
	{
		/// <summary>
		/// Der Code
		/// </summary>
		public CodeExpression<Proband> Code { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Windows.Forms.Form"/> class.
		/// </summary>
		/// <remarks></remarks>
		public CodeView(CodeExpression<Proband> code)
		{
			InitializeComponent();
			Code = code;
			if (code != null)
			{
				string rtf = RenderToRtf(code);
				richTextBoxCode.Rtf = rtf;
			}
		}

		/// <summary>
		/// Handles the Click event of the buttonClose control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ButtonCloseClick(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Rendert den Code als Rich Text
		/// </summary>
		/// <param name="code">Der Code</param>
		/// <returns></returns>
		private string RenderToRtf(CodeExpression<Proband> code)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(@"{\rtf1\ansi\deff0");
			// crimson --> #DC143C
			// dodger --> #1E90FF
			builder.AppendLine(@"{\colortbl;\red255\green255\blue255;\red220\green20\blue60;\red30\green144\blue255;}");
			builder.AppendLine(@"{\fonttbl\f0\fswiss Consolas;}\f0\cf1");
			RenderToRtf(code, 0, builder);
			builder.AppendLine("}");
			return builder.ToString();
		}

		/// <summary>
		/// Erzeugt eine Einrückung
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		private string GenerateIndentation(int level)
		{
			if (level == 0) return String.Empty;
			return String.Concat(Enumerable.Repeat("    ", level));
		}

		/// <summary>
		/// Rendert den Code als Rich Text
		/// </summary>
		/// <param name="code">Der Code</param>
		/// <param name="indentationLevel">The indentation level.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		private void RenderToRtf(CodeExpression<Proband> code, int indentationLevel, StringBuilder builder)
		{
			string indent = GenerateIndentation(indentationLevel);

			if (code is ConditionalCodeExpression<Proband>)
			{
				ConditionalCodeExpression<Proband> conditionalCode = (ConditionalCodeExpression<Proband>) code;
				string decisionName = conditionalCode.Decision.Name;
				
				builder.Append(indent);
				builder.Append("if (");
				builder.Append("\\cf3{\\b ");
				builder.Append(decisionName + "()");
				builder.AppendLine("}\\cf1)\\par");
				
				builder.Append(indent);
				builder.AppendLine("\\{\\par");

				RenderToRtf(conditionalCode.LeftAction, indentationLevel + 1, builder);

				builder.Append(indent);
				builder.AppendLine("\\}\\par");

				builder.Append(indent);
				builder.AppendLine("else\\par");

				builder.Append(indent);
				builder.AppendLine("\\{\\par");

				RenderToRtf(conditionalCode.RightAction, indentationLevel + 1, builder);

				builder.Append(indent);
				builder.AppendLine("\\}\\par");
			}
			else
			{
				string terminalName = code.Terminal.Name;

				builder.Append(indent);
				builder.Append("\\cf2{\\b ");
				builder.Append(terminalName + "();");
				builder.AppendLine("}\\cf1\\par");
			}
		}
	}
}
